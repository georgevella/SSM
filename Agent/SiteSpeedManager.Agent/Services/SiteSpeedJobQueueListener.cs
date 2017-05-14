using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Amazon.SQS;
using Amazon.SQS.Model;
using Glyde.Configuration;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using NLog;
using SiteSpeedManager.Models.SiteSpeed;
using SiteSpeedManager.Transport;

namespace SiteSpeedManager.Agent.Services
{
    public class SiteSpeedJobQueueListener : ISiteSpeedJobQueueListener, IDisposable
    {
        private readonly IAmazonSQS _sqsClient;
        private readonly IMessageSerializer<SiteSpeedJobDetails> _serializer;
        private readonly ISiteSpeedProcess _siteSpeedProcess;
        private readonly ILogger _logger;
        private CancellationTokenSource _cancellationTokenSource = null;
        private readonly ConcurrentDictionary<string, Task> _taskList = new ConcurrentDictionary<string, Task>();

        public SiteSpeedJobQueueListener(
            IAmazonSQS sqsClient,
            IConfigurationService configurationService,
            IMessageSerializer<SiteSpeedJobDetails> serializer,
            ISiteSpeedProcess siteSpeedProcess,
            ILogger logger)
        {
            _sqsClient = sqsClient;
            _serializer = serializer;
            _siteSpeedProcess = siteSpeedProcess;
            _logger = logger;
        }

        public AgentStatus IsRunning
        {
            get
            {
                if (_taskList.Count > 0 && _cancellationTokenSource != null &&
                    !_cancellationTokenSource.IsCancellationRequested)
                {
                    return AgentStatus.Enabled;
                }

                return AgentStatus.Disabled;
            }
        }

        public async Task Start(IEnumerable<string> countryList)
        {
            lock (this)
            {
                if (_cancellationTokenSource != null)
                {
                    // TODO: log and maybe throw exception
                    return;
                }

                _cancellationTokenSource = new CancellationTokenSource();
            }

            foreach (var country in countryList)
            {
                var queueUrl = await GetQueueUrl(country);

#pragma warning disable 4014
                _taskList.TryAdd(country,
                    Task.Factory.StartNew(
                        ListeningLoop,
                        queueUrl,
                        _cancellationTokenSource.Token,
                        TaskCreationOptions.LongRunning,
                        TaskScheduler.Default)
                );
#pragma warning restore 4014
            }
        }

        private async Task<string> GetQueueUrl(string countryId)
        {
            _logger.Trace("SiteSpeedJobQueueListener::GetQueueUrl() >>");

            var queues = await _sqsClient.ListQueuesAsync(new ListQueuesRequest()
            {
                QueueNamePrefix = "sitespeed"
            });

            var expectedQueueName = $"sitespeed_{countryId}";

            var queueUrl = queues.QueueUrls.FirstOrDefault(url => url.EndsWith(expectedQueueName));

            if (queueUrl != null)
            {
                _logger.Trace($"SiteSpeedJobQueueListener::GetQueueUrl() << {queueUrl}");
                return queueUrl;
            }

            var result = await _sqsClient.CreateQueueAsync(expectedQueueName);
            queueUrl = result.QueueUrl;

            _logger.Trace($"SiteSpeedJobQueueListener::GetQueueUrl() << {queueUrl}");
            return queueUrl;
        }

        public void Stop()
        {
            lock (this)
            {
                if (_cancellationTokenSource == null)
                    return;

                _cancellationTokenSource.Cancel();
                _cancellationTokenSource.Dispose();
                _cancellationTokenSource = null;

                _taskList.Clear();
            }

            // TODO: maybe wait until task is properly shut down
        }

        private void ListeningLoop(object queueUrl)
        {
            _logger.Trace($"SiteSpeedJobQueueListener::ListeningLoop('{queueUrl}') >>");
            while (!_cancellationTokenSource.IsCancellationRequested)
            {
                var receiveMessageResponse = _sqsClient.ReceiveMessageAsync(new ReceiveMessageRequest()
                {
                    QueueUrl = queueUrl.ToString(),
                    MaxNumberOfMessages = 1,
                    VisibilityTimeout = 60,
                    WaitTimeSeconds = 20
                }, _cancellationTokenSource.Token).Result;


                if (receiveMessageResponse.Messages.Count == 0)
                {
                    _logger.Warn("ReceiveMessageAsync returned without any messages.");
                    continue;
                }

                _logger.Debug($"Recieved message from ({queueUrl})");

                var message = receiveMessageResponse.Messages.FirstOrDefault();

                var details = _serializer.DeserializeFromString(message.Body);

                _siteSpeedProcess.Run(details);

                //_sqsClient.DeleteMessageAsync(queueUrl.ToString(), message.ReceiptHandle);
            }

            _logger.Trace($"SiteSpeedJobQueueListener::ListeningLoop('{queueUrl}') <<");
        }

        public void Dispose()
        {
            _cancellationTokenSource?.Dispose();
        }
    }
}
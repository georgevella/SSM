using System;
using System.Threading.Tasks;
using Amazon.SQS;
using NLog;
using Quartz;
using SiteSpeedManager.Master.Services.Jobs;
using SiteSpeedManager.Models.SiteSpeed;
using SiteSpeedManager.Transport;

namespace SiteSpeedManager.Master.Jobs
{
    public class SiteSpeedJob : IJob
    {
        private readonly IAmazonSQS _sqsClient;
        private readonly IMessageFactory<SiteSpeedJobDetails> _messageFactory;
        private readonly ILogger _logger;

        public SiteSpeedJob(IAmazonSQS sqsClient, IMessageFactory<SiteSpeedJobDetails> messageFactory, ILogger logger)
        {
            _sqsClient = sqsClient;
            _messageFactory = messageFactory;
            _logger = logger;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            _logger.Trace("SiteSpeedJob::Execute() >>");

            var domain = (Uri)context.JobDetail.JobDataMap[SiteSpeedJobDataKeys.Domain];
            var path = (string)context.JobDetail.JobDataMap[SiteSpeedJobDataKeys.Path];
            var settings = (SiteSpeedSettings)context.JobDetail.JobDataMap[SiteSpeedJobDataKeys.Settings];
            var country = (string)context.JobDetail.JobDataMap[SiteSpeedJobDataKeys.Country];

            _logger.Debug("Building message over sqs");
            var request = await _messageFactory.CreateSendMessageRequest(country, new SiteSpeedJobDetails()
            {
                Uri = new Uri(domain, path),
                Settings = settings
            });

            _logger.Debug("Sending message over sqs");
            var response = await _sqsClient.SendMessageAsync(request);

            _logger.Info($"SQS Response received -> {response.HttpStatusCode}");
            _logger.Trace("SiteSpeedJob::Execute() <<");
        }
    }
}
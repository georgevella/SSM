using System;
using System.Threading.Tasks;
using Amazon.SQS;
using NLog;
using Quartz;
using SiteSpeedController.Master.Services.Jobs;
using SiteSpeedController.Master.Services.Transport;
using SiteSpeedManager.Models.SiteSpeed;

namespace SiteSpeedController.Master.Jobs
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
            _logger.Info("Starting job...");

            var domain = (Uri)context.JobDetail.JobDataMap[SiteSpeedJobDataKeys.Domain];
            var path = (string)context.JobDetail.JobDataMap[SiteSpeedJobDataKeys.Path];
            var settings = (SiteSpeedSettings)context.JobDetail.JobDataMap[SiteSpeedJobDataKeys.Settings];
            var country = (string)context.JobDetail.JobDataMap[SiteSpeedJobDataKeys.Country];


            var request = await _messageFactory.CreateSendMessageRequest(country, new SiteSpeedJobDetails()
            {
                Uri = new Uri(domain, path),
                Settings = settings
            });

            var response = await _sqsClient.SendMessageAsync(request);

            _logger.Info($"SQS Response received -> {response.HttpStatusCode}");
        }
    }
}
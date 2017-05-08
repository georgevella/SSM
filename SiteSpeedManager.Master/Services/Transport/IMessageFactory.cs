using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Amazon.SQS;
using Amazon.SQS.Model;
using Glyde.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace SiteSpeedController.Master.Services.Transport
{
    public interface IMessageFactory<in TContent>
    {
        Task<SendMessageRequest> CreateSendMessageRequest(string countryId, TContent content);
    }

    internal class MessageFactory<TContent> : IMessageFactory<TContent>
    {
        private readonly IAmazonSQS _sqsClient;
        private readonly IMessageSerializer<TContent> _messageSerializer;

        public MessageFactory(IAmazonSQS sqsClient, IMessageSerializer<TContent> messageSerializer)
        {
            _sqsClient = sqsClient;
            _messageSerializer = messageSerializer;
        }

        public async Task<SendMessageRequest> CreateSendMessageRequest(string countryId, TContent content)
        {
            // ensure queue is available
            var queues = await _sqsClient.ListQueuesAsync(new ListQueuesRequest()
            {
                QueueNamePrefix = "sitespeed"
            });

            string expectedQueueName = $"sitespeed_{countryId}";
            var queueUrl = queues.QueueUrls.FirstOrDefault(url => url.EndsWith(expectedQueueName));
            if (queueUrl == null)
            {
                var result = await _sqsClient.CreateQueueAsync(expectedQueueName);
                queueUrl = result.QueueUrl;
            }

            // convert content and prep sqs message request
            var message = _messageSerializer.SerializeAsString(content);

            return new SendMessageRequest()
            {
                QueueUrl = queueUrl,
                MessageBody = message
            };
        }
    }
}
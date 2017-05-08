using Amazon.SQS.Model;

namespace SiteSpeedController.Master.Services.Transport
{
    public interface IMessageTx
    {
        void Transmit(SendMessageRequest sendMessageRequest);
    }
}
using Amazon.SQS.Model;

namespace SiteSpeedManager.Master.Services.Transport
{
    public interface IMessageTx
    {
        void Transmit(SendMessageRequest sendMessageRequest);
    }
}
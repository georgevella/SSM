using Amazon.SQS.Model;

namespace SiteSpeedManager.Transport
{
    public interface IMessageTx
    {
        void Transmit(SendMessageRequest sendMessageRequest);
    }
}
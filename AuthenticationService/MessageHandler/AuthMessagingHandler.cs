using Shared.DTO.RabbitMQ;
using Shared.Messaging;

namespace AuthenticationService.MessageHandler
{
    public class AuthMessagingHandler : IMessageHandler
    {
        public Task HandleMessage(RabbitMqMessage message)
        {
            throw new NotImplementedException();
        }
    }
}

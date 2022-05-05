using Shared.DTO.RabbitMQ;
using Shared.Messaging;
using System.Text.Json;

namespace UserService.MessageHandler
{
    public class UserMessagingHandler : IMessageHandler
    {
        public Task HandleMessage(RabbitMqMessage message)
        {
            Console.WriteLine(JsonSerializer.Serialize(message));
            return Task.CompletedTask;
        }
    }
}

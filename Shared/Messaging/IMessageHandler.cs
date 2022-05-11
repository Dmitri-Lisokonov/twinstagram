using Shared.DTO.RabbitMQ;
using System.Text.Json;

namespace Shared.Messaging
{
    
    public interface IMessageHandler
    {
        Task HandleMessage(RabbitMqMessage message);
    }

}

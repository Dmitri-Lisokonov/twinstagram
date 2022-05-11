using Shared.Messaging;

namespace Shared.DTO.RabbitMQ
{
    public class RabbitMqMessage
    {
        public MessageAction MessageAction { get; set; }

        public string Data { get; set; }

        public RabbitMqMessage(MessageAction messageAction, string data)
        {
            MessageAction = messageAction;
            Data = data;
        }
    }
}

using Shared.DTO.RabbitMQ;
using System.Text.Json;

namespace Shared.Messaging
{
    public class MessagePublisher
    {
        private readonly MessagingQueueList _queues;
        private readonly RabbitMqConnection _connection;
        
        public MessagePublisher(RabbitMqConnection connection, MessagingQueueList queues)
        {
            _connection = connection;
            _queues = queues;
        }
        
        public Task SendMessage<T>(T message)
        {
            if(_queues.PublishQueues.Count > 0)
            {
                foreach (var queue in _queues.PublishQueues)
                {
                    using var channel = _connection.CreateChannel();
                    channel.QueueDeclare(queue: queue.Name.ToString(),
                                         durable: true,
                                         exclusive: false,
                                         autoDelete: false,
                                         arguments: null);
                    var header = channel.CreateBasicProperties();
                    header.ContentType = "application/json";
                    header.DeliveryMode = 2;
                    var body = JsonSerializer.SerializeToUtf8Bytes(message);
                    channel.BasicPublish(exchange: "",
                                         routingKey: queue.Name.ToString(),
                                         basicProperties: header,
                                         mandatory: true,
                                         body: body);
                }
            }
            return Task.CompletedTask;
        }
    }
}

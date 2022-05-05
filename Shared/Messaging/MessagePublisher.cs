using Shared.DTO.RabbitMQ;
using System.Text.Json;

namespace Shared.Messaging
{
    public class MessagePublisher
    {
        private readonly PublishQueue _publishQueue;
        private readonly RabbitMqConnection _connection;
        
        public MessagePublisher(RabbitMqConnection connection, PublishQueue publishQueue)
        {
            _connection = connection;
            _publishQueue = publishQueue;
        }
        
        public Task SendMessage<T>(T message)
        {
            using var channel = _connection.CreateChannel();
            channel.QueueDeclare(queue: _publishQueue.Name,
                                 durable: true,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            var header = channel.CreateBasicProperties();
            header.ContentType = "application/json";
            header.DeliveryMode = 2;
            var body = JsonSerializer.SerializeToUtf8Bytes(message);
            
            channel.BasicPublish(exchange: "",
                                 routingKey: _publishQueue.Name,
                                 basicProperties: header,
                                 mandatory: true,
                                 body: body);
            
            return Task.CompletedTask;
        }
    }
}

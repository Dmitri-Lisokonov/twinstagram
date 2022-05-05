using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Shared.DTO.RabbitMQ;
using System.Text;

namespace Shared.Messaging
{
    public class QueueConsumer : IHostedService, IDisposable
    {
        private readonly ConsumeQueue _consumeQueue;
        private readonly RabbitMqConnection _connection;
        private IModel _channel;

        public QueueConsumer(RabbitMqConnection connection, ConsumeQueue consumeQueue)
        {
            _consumeQueue = consumeQueue;
            _connection = connection;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _channel = _connection.CreateChannel();
            _channel.QueueDeclare(queue: _consumeQueue.Name,
                                 durable: true,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine(message);

            };

            _channel.BasicConsume(queue: _consumeQueue.Name,
                                 noLocal: false,
                                 exclusive: false,
                                 arguments: null,
                                 consumerTag: "",
                                 autoAck: true,
                                 consumer: consumer);
           
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _channel.Dispose();
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _channel?.Dispose();
        }
    }
}

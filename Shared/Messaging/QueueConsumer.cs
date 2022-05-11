using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Shared.DTO.RabbitMQ;
using System.Text;
using System.Text.Json;

namespace Shared.Messaging
{
    public class QueueConsumer : IHostedService, IDisposable
    {
        private readonly MessagingQueueList _queues;
        private readonly RabbitMqConnection _connection;
        private readonly IMessageHandler _messageHandler;
        private IModel _channel;

        public QueueConsumer(RabbitMqConnection connection, MessagingQueueList queues, IMessageHandler messageHandler)
        {
            _messageHandler = messageHandler;
            _queues = queues;
            _connection = connection;
        }
        
        public Task StartAsync(CancellationToken cancellationToken)
        {
            if(_queues.ConsumeQueues.Count() > 0)
            {
                foreach (var queue in _queues.ConsumeQueues)
                {
                    _channel = _connection.CreateChannel();
                    _channel.QueueDeclare(queue: queue.Name.ToString(),
                                         durable: true,
                                         exclusive: false,
                                         autoDelete: false,
                                         arguments: null);
                    var consumer = new EventingBasicConsumer(_channel);
                    consumer.Received += (model, ea) =>
                    {
                        var body = ea.Body.ToArray();
                        var message = Encoding.UTF8.GetString(body);
                        SendToHandler(message);

                    };
                    _channel.BasicConsume(queue: queue.Name.ToString(),
                                         noLocal: false,
                                         exclusive: false,
                                         arguments: null,
                                         consumerTag: "",
                                         autoAck: true,
                                         consumer: consumer);
                }
            }
            return Task.CompletedTask;
        }

        public void SendToHandler(string message)
        {
            var parsedMessage = JsonSerializer.Deserialize<RabbitMqMessage>(message);
            _messageHandler.HandleMessage(parsedMessage);
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

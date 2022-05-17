using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Messaging
{
    public class RabbitMqConnection : IDisposable
    {
        private IConnection _connection;

        private IConnection CreateConnection()
        {
            if(_connection is null)
            {
                var endpoints = new List<AmqpTcpEndpoint>
                {
                    new AmqpTcpEndpoint("rabbitmq"),
                    new AmqpTcpEndpoint("localhost"),
                    new AmqpTcpEndpoint("10.1.243.195")
                };
                var connectionFactory = new ConnectionFactory
                {
                    Port = AmqpTcpEndpoint.UseDefaultPort,
                    UserName = "guest",
                    Password = "guest"
                };
                _connection = connectionFactory.CreateConnection(endpoints);
            }

            return _connection;
        }

        public IModel CreateChannel()
        {
            var connection = CreateConnection();
            return connection.CreateModel();
        }
        
        public void Dispose()
        {
            if (_connection is not null)
            {
                _connection.Dispose();
            }
        }
    }
}

using Microsoft.Extensions.DependencyInjection;
using Shared.DTO.RabbitMQ;

namespace Shared.Messaging
{
    public static class MessagingService
    {
        public static void AddMessagingService(this IServiceCollection services, PublishQueue publishQueue, ConsumeQueue consumeQueue)
        {
            var connection = new RabbitMqConnection();
            services.AddSingleton(connection);
            
            if(publishQueue is not null)
            {
                services.AddSingleton(publishQueue);
                services.AddScoped<MessagePublisher>();
            }
            if (consumeQueue is not null)
            {
                services.AddSingleton(consumeQueue);
                services.AddHostedService<QueueConsumer>();
            }
        }
    }
}

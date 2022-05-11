using Microsoft.Extensions.DependencyInjection;
using Shared.DTO.RabbitMQ;

namespace Shared.Messaging
{
    public static class MessagingService
    {
        public static void AddMessagingService<THandler>(this IServiceCollection services, MessagingQueueList queues, THandler handler) where THandler : class
        {
            var connection = new RabbitMqConnection();
            services.AddSingleton(connection);
            services.AddSingleton(queues);
            services.AddSingleton(handler);
            services.AddScoped<MessagePublisher>();
            services.AddHostedService<QueueConsumer>();
        }
    }
}

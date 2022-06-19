using MessageService.Context;
using Shared.DTO.RabbitMQ;
using Shared.Messaging;
using Shared.Models.User;
using System.Text.Json;

namespace UserService.MessageHandler
{
    public class MessageMessagingHandler : IMessageHandler
    {
        readonly MessageServiceDatabaseContext _Dbcontext;
        public MessageMessagingHandler(MessageServiceDatabaseContext dbcontext)
        {
            _Dbcontext = dbcontext;
        }

        public async Task HandleMessage(RabbitMqMessage message)
        {
            if (message.MessageAction.Equals(MessageAction.Follow))
            {
                var follow = JsonSerializer.Deserialize<Follow>(message.Data);
                await _Dbcontext.AddAsync(follow);
                await _Dbcontext.SaveChangesAsync();
            }
            else if(message.MessageAction.Equals(MessageAction.Unfollow))
            {
                var unfollow = JsonSerializer.Deserialize<Follow>(message.Data);
                _Dbcontext.Remove(unfollow);
                await _Dbcontext.SaveChangesAsync();
            }
        }
    }
}

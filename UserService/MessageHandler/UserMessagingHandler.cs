using Shared.DTO.RabbitMQ;
using Shared.Messaging;
using Shared.Models.User;
using System.Text.Json;
using UserService.Context;

namespace UserService.MessageHandler
{
    public class UserMessagingHandler : IMessageHandler
    {
        UserServiceDatabaseContext _Dbcontext;
        public UserMessagingHandler(UserServiceDatabaseContext dbcontext)
        {
            _Dbcontext = dbcontext;
        }
        
        public async Task HandleMessage(RabbitMqMessage message)
        {
            if (message.MessageAction.Equals(MessageAction.Register))
            {
   
                    var user = JsonSerializer.Deserialize<ApplicationUser>(message.Data);
                    await _Dbcontext.Users.AddAsync(user);
                    var result = await _Dbcontext.SaveChangesAsync();
            }
        }
    }
}

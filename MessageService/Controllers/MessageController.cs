using MessageService.Context;
using MessageService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace MessageService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MessageController : Controller
    {
        private readonly MessageServiceDatabaseContext _dbContext;

        public MessageController(MessageServiceDatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("user/{userId}")]
        //TODO: Considering removing List and return just the Message
        public async Task<ActionResult<IEnumerable<List<Message>>>> GetMessages(int userId)
        {
#pragma warning disable CS8604 // Possible null reference argument.
            var messages = await _dbContext.Messages
               .Where(x => x.UserId == userId)
               .ToListAsync();
#pragma warning restore CS8604 // Possible null reference argument.

            if (messages.Any())
            {
                return Ok(messages);
            }
            else
            {
                return Ok("No messages found");
            }
        }

        [HttpPost]
        public async Task<ActionResult> CreateNewMessage(Message message)
        {
            //TODO: Add Authentication
            message.CreatedDate = DateTime.Now;
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            await _dbContext.Messages.AddAsync(message);
#pragma warning restore CS8602 // Dereference of a possibly null reference.
            var result = await _dbContext.SaveChangesAsync();

            if (result > 0)
            {
                return Ok();
            }
            else
            {
                return BadRequest("Something went wrong creating the message... Please try again");
            }
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteMessage(Message message)
        {
            //TODO: Add Authentication
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            _dbContext.Messages.Remove(message);
#pragma warning restore CS8602 // Dereference of a possibly null reference.
            var result = await _dbContext.SaveChangesAsync();

            if(result > 0)
            {
                return Ok();
            }
            else
            {
                return NotFound("The message you want to delete was not found");
            }
        }

        [HttpPost("/feed/{userId}")]
        public async Task<ActionResult<IEnumerable<List<Message>>>> GetAllFollowerMessages(List<int> followingIdList)
        {
            //TODO: Add Authentication
            List<Message> feed = new List<Message>();

            if(followingIdList is null)
            {
                return BadRequest("Request is suposed to include a list of follower IDs");
            }
            else if(followingIdList.Count > 0)
            {
                foreach (var followingId in followingIdList)
                {
#pragma warning disable CS8604 // Possible null reference argument.
                    var messages = await _dbContext.Messages
                        .Where(x => x.UserId == followingId)
                        .ToListAsync();
#pragma warning restore CS8604 // Possible null reference argument.

                    feed.AddRange(messages);
                }

                List<Message> sortedFeed = feed.OrderBy(o => o.CreatedDate).ToList();
                return Ok(feed);
            }
            else
            {
                return Ok("You aren't following any accounts");
            }
  
        }
    }
}

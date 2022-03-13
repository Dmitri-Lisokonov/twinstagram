using MessageService.Context;
<<<<<<< HEAD
using Microsoft.AspNetCore.Mvc;
=======
using MessageService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
>>>>>>> e884d9240d91d31b7bb0866a701fb38791c9aa11


namespace MessageService.Controllers
{
<<<<<<< HEAD
    //TODO: Add try catch handling
=======
>>>>>>> e884d9240d91d31b7bb0866a701fb38791c9aa11
    [Route("[controller]")]
    [ApiController]
    public class MessageController : Controller
    {
        private readonly MessageServiceDatabaseContext _dbContext;

        public MessageController(MessageServiceDatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }
<<<<<<< HEAD
=======

        [HttpGet("/{userId}")]
        public async Task<ActionResult<IEnumerable<List<Message>>>> GetMessages(int userId)
        {
            var messages = await _dbContext.Messages
               .Where(x => x.UserId == userId)
               .ToListAsync();

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
            message.CreatedDate = DateTime.Now
            await _dbContext.Messages.AddAsync(message);
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
            _dbContext.Messages.Remove(message);
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
                    var messages = await _dbContext.Messages
                        .Where(x => x.UserId == followingId)
                        .ToListAsync();

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
>>>>>>> e884d9240d91d31b7bb0866a701fb38791c9aa11
    }
}

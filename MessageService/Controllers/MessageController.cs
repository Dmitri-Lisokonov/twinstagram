using AutoMapper;
using MessageService.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shared.DTO.Message;
using Shared.DTO.Response;
using Shared.Models.Message;
using System.Security.Claims;

namespace MessageService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MessageController : Controller
    {
        private readonly MessageServiceDatabaseContext _dbContext;
        private readonly Mapper _mapper;

        public MessageController(MessageServiceDatabaseContext dbContext)
        {
            _dbContext = dbContext;

            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Message, MessageDto>();
                cfg.CreateMap<MessageDto, Message>();
                cfg.CreateMap<CreateMessage, Message>();
            });

            _mapper = new Mapper(config);
        }

        [HttpGet("user")]
        [Authorize]
        public async Task<IActionResult> GetMessages(Guid userId)
        {

            var messages = await _dbContext.Messages
               .Where(x => x.UserId == userId)
               .ToListAsync();

            var messagesDto = _mapper.Map<IEnumerable<Message>, IEnumerable<MessageDto>>(messages);

            if (messages.Any())
            {
                return Ok(new ResponseMessage<IEnumerable<MessageDto>>(messagesDto, ResponseStatus.Success.ToString()));
            }
            else
            {
                return Ok(new ResponseMessage<string>("No messages found", ResponseStatus.NotFound.ToString()));
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateNewMessage(CreateMessage message)
        {
            var currentUserId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var currentUserName = HttpContext.User.FindFirstValue(ClaimTypes.Name);
            message.CreatedDate = DateTime.Now;
            message.UserId = Guid.Parse(currentUserId);
            var messageToCreate = _mapper.Map<Message>(message);
            await _dbContext.Messages.AddAsync(messageToCreate);
            await _dbContext.SaveChangesAsync();
            return Ok(new ResponseMessage<string>("message created", ResponseStatus.Success.ToString()));

        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> DeleteMessage(MessageDto message)
        {
            var currentUserId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            // Does this work?
            message.UserId = Guid.Parse(currentUserId);
            var messageToDelete = _mapper.Map<Message>(message);
            _dbContext.Messages.Remove(messageToDelete);
            // Check if messages was deleted otherwise send reponse that it was not deleted
            await _dbContext.SaveChangesAsync();
            return Ok(new ResponseMessage<string>("Your message has been deleted", ResponseStatus.Success.ToString()));
        }

        [HttpGet("feed")]
        [Authorize]
        public async Task<IActionResult> GetFeed()
        {
            var currentUserId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var currentUserName = HttpContext.User.FindFirstValue(ClaimTypes.Name);
            List<Message> feed = new List<Message>();

            var following = await _dbContext.Followers.Where(x => x.UserId == Guid.Parse(currentUserId)).ToListAsync();
            if (following.Any())
            {
                foreach (var follower in following)
                {
                    var messages = await _dbContext.Messages.Where(x => x.UserId == follower.FollowUserId).ToListAsync();
                    feed.AddRange(messages);
                }

                List<Message> sortedFeed = feed.OrderByDescending(o => o.CreatedDate).ToList();
                var sortedFeedDto = _mapper.Map<IEnumerable<Message>, IEnumerable<MessageDto>>(sortedFeed);
                return Ok(new ResponseMessage<IEnumerable<MessageDto>>(sortedFeedDto, ResponseStatus.Success.ToString()));
            }
            else
            {
                return NotFound(new ResponseMessage<string>("You are not following anyone yet", ResponseStatus.NotFound.ToString()));
            }
        }
    }
}

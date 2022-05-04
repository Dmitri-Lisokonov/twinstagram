using AutoMapper;
using MessageService.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shared.DTO.Message;
using Shared.DTO.Response;
using Shared.Models.Message;

namespace MessageService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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

        [HttpGet("user/{userId}")]
        [Authorize]
        public async Task<IActionResult> GetMessages(Guid userId)
        {
            var messages = await _dbContext.Messages
               .Where(x => x.UserId == userId)
               .ToListAsync();

            var messagesDto = _mapper.Map<IEnumerable<Message>, IEnumerable<MessageDto>>(messages);

            if (messages.Any())
            {
                return Ok(messagesDto);
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
            //TODO: UserId should be taken from the token
            message.CreatedDate = DateTime.Now;
            var messageToCreate = _mapper.Map<Message>(message);
            var createdMessage = await _dbContext.Messages.AddAsync(messageToCreate);
            var result = await _dbContext.SaveChangesAsync();
            var createdMessageDto = _mapper.Map<MessageDto>(createdMessage.Entity);
            return Ok(createdMessageDto);

        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> DeleteMessage(MessageDto message)
        {
            var messageToDelete = _mapper.Map<Message>(message);
            _dbContext.Messages.Remove(messageToDelete);
            await _dbContext.SaveChangesAsync();
            return Ok(new ResponseMessage<string>("Your message has been deleted", ResponseStatus.Success.ToString()));
        }

        [HttpPost("feed/{userId}")]
        [Authorize]
        public async Task<IActionResult> GetAllFollowerMessages(List<Guid> followingIdList)
        {
            List<Message> feed = new List<Message>();

            if (followingIdList is null)
            {
                return BadRequest(new ResponseMessage<string>("You are not following anyone yet", ResponseStatus.Error.ToString()));
            }
            else if (followingIdList.Count > 0)
            {
                foreach (var followingId in followingIdList)
                {
                    var messages = await _dbContext.Messages
                        .Where(x => x.UserId == followingId)
                        .ToListAsync();

                    feed.AddRange(messages);
                }

                List<Message> sortedFeed = feed.OrderBy(o => o.CreatedDate).ToList();
                var sortedFeedDto = _mapper.Map<IEnumerable<Message>, IEnumerable<MessageDto>>(sortedFeed);
                return Ok(sortedFeedDto);
            }
            else
            {
                return NotFound(new ResponseMessage<string>("You are not following anyone yet", ResponseStatus.NotFound.ToString()));
            }

        }
    }
}

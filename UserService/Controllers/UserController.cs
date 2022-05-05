using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shared.DTO.RabbitMQ;
using Shared.DTO.Response;
using Shared.DTO.User;
using Shared.Messaging;
using Shared.Models.User;
using System.Security.Claims;
using UserService.Context;

namespace UserService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly UserServiceDatabaseContext _dbContext;
        private readonly Mapper _mapper;
        
        public UserController(UserServiceDatabaseContext dbContext)
        {
            _dbContext = dbContext;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ApplicationUser, ApplicationUserDto>();
                cfg.CreateMap<ApplicationUserDto, ApplicationUser>();
            });

            _mapper = new Mapper(config);
        }
         
        [HttpGet]
        //[Authorize]
        public async Task<IActionResult> GetUser(string username)
        {
            var user = await _dbContext.Users
                .FirstOrDefaultAsync(x => x.Username == username);

            if (user is not null)
            {
                var userDto = _mapper.Map<ApplicationUser, ApplicationUserDto>(user);

                var followerCount = _dbContext.Followers
                    .Where(x => x.FollowUserId == user.Id)
                    .Count();

                var followingCount = _dbContext.Followers
                    .Where(x => x.UserId == user.Id)
                    .Count();

                userDto.FollowerCount = followerCount;
                userDto.FollowingCount = followingCount;

                return Ok(new ResponseMessage<ApplicationUserDto>(userDto, ResponseStatus.Success.ToString()));
            }
            else
            {
                return NotFound(new ResponseMessage<string>("No user found with specified username", ResponseStatus.NotFound.ToString()));
            }
        }

        
        [HttpGet("Followers")]
        //[Authorize]
        public async Task<IActionResult> GetUserFollowers(string username)
        {
            //get user
            var user = await _dbContext.Users
                .FirstOrDefaultAsync(x => x.Username == username);


            if (user is not null)
            {
                // Get all follows
                var followers = await _dbContext.Followers
                   .Where(x => x.FollowUserId == user.Id)
                   .ToListAsync();

                List<ApplicationUserDto> followerList = new List<ApplicationUserDto>();

                if (followers.Count() > 0)
                {
                    // Get users for each follow
                    foreach (var follower in followers)
                    {
                        var result = await _dbContext.Users
                            .FirstOrDefaultAsync(x => x.Id == follower.UserId);

                        if (result is not null)
                        {
                            var resultDto = _mapper.Map<ApplicationUser, ApplicationUserDto>(result);
                            followerList.Add(resultDto);
                        }
                    }
                }
                return Ok(new ResponseMessage<List<ApplicationUserDto>>(followerList, ResponseStatus.Success.ToString()));
            }
            else
            {
                return NotFound(new ResponseMessage<string>("No user found with specified username", ResponseStatus.NotFound.ToString()));
            }
        }

        
        [HttpGet("Following")]
        //[Authorize]
        public async Task<IActionResult> GetUserFollowing(string username)
        {
            var user = await _dbContext.Users
                .FirstOrDefaultAsync(x => x.Username == username);

            if (user is not null)
            {
                var following = await _dbContext.Followers
                .Where(x => x.UserId == user.Id)
                .ToListAsync();

                List<ApplicationUserDto> followingList = new List<ApplicationUserDto>();

                foreach (var follow in following)
                {
                    var result = await _dbContext.Users
                        .FirstOrDefaultAsync(x => x.Id == follow.FollowUserId);

                    if (result is not null)
                    {
                        var userDto = _mapper.Map<ApplicationUser, ApplicationUserDto>(result);
                        followingList.Add(userDto);
                    }
                }

                return Ok(new ResponseMessage<List<ApplicationUserDto>>(followingList, ResponseStatus.Success.ToString()));
            }
            else
            {
                return NotFound(new ResponseMessage<string>("No user found with specified username", ResponseStatus.NotFound.ToString()));
            }
        }

        
        [HttpGet("Follow")]
        //[Authorize]
        public async Task<IActionResult> FollowUser(Guid userId)
        {
            var currentUserId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            
            if(Guid.Parse(currentUserId) == userId)
            {
                return BadRequest(new ResponseMessage<string>("You cannot follow yourself", ResponseStatus.BadRequest.ToString()));
            }
            
            var currentUserGuid = Guid.Parse(currentUserId);
            var alreadyFollowing = await _dbContext.Followers
                .FirstOrDefaultAsync(x => x.UserId == currentUserGuid && x.FollowUserId == userId);

            var userExists = await _dbContext.Users
                .FirstOrDefaultAsync(x => x.Id == userId);

            if (alreadyFollowing is null && userExists is not null)
            {
                var result = await _dbContext.Followers.AddAsync(new Follow(currentUserGuid, userId));
                _dbContext.SaveChanges();

                return Ok(new ResponseMessage<string>("You are now following this user", ResponseStatus.Success.ToString()));
            }
            else if (alreadyFollowing is not null)
            {
                _dbContext.Followers.Remove(alreadyFollowing);
                await _dbContext.SaveChangesAsync();
                return Ok(new ResponseMessage<string>("You have unfollowed this user", ResponseStatus.Success.ToString()));
            }
            else
            {
                return NotFound(new ResponseMessage<string>("No user found with specified username", ResponseStatus.NotFound.ToString()));
            }
        }
    }
}

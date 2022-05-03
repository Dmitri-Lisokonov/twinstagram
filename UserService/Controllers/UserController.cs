using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserService.Context;
using UserService.Models;
using UserService.Models.DTO;

namespace UserService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly UserServiceDatabaseContext _dbContext;

        public UserController(UserServiceDatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("{username}")]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetUser(string username)
        {
#pragma warning disable CS8604 // Possible null reference argument.
            var user = await _dbContext.Users
                .FirstOrDefaultAsync(x => x.Username == username);
#pragma warning restore CS8604 // Possible null reference argument.

            if (user == null)
            {
                return NotFound("No user found with that username");
            }
            else
            {
                UserDTO userDto = new UserDTO(user.Id, user.Username, user.Name, user.Bio);
                return Ok(userDto);
            }
        }
        [HttpGet("Followers/{username}")]
        public async Task<ActionResult<IEnumerable<List<UserDTO>>>> GetUserFollowers(string username)
        {
#pragma warning disable CS8604 // Possible null reference argument.
            var user = await _dbContext.Users
                .FirstOrDefaultAsync(x => x.Username == username);
#pragma warning restore CS8604 // Possible null reference argument.

            if (user is not null)
            {
#pragma warning disable CS8604 // Possible null reference argument.
                var followers = await _dbContext.Followers
                   .Where(x => x.UsernameToFollow == user.Username)
                   .ToListAsync();
#pragma warning restore CS8604 // Possible null reference argument.

                List<UserDTO> followerList = new List<UserDTO>();

                if (followers.Count() > 0)
                {
                    foreach (var follower in followers)
                    {
                        var result = await _dbContext.Users
                            .FirstOrDefaultAsync(x => x.Username == follower.Username);

                        if (result is not null)
                        {
                            followerList.Add(new UserDTO(result.Id, result.Username, result.Name, result.Bio));
                        }
                    }
                }
                return Ok(followerList);
            }
            else
            {
                return BadRequest("No user found with that username");
            }
        }

        [HttpGet("Following/{username}")]
        public async Task<ActionResult<IEnumerable<List<UserDTO>>>> GetUserFollowing(string username)
        {
#pragma warning disable CS8604 // Possible null reference argument.
            var user = await _dbContext.Users
                .FirstOrDefaultAsync(x => x.Username == username);
#pragma warning restore CS8604 // Possible null reference argument.

            if(user is not null)
            {
#pragma warning disable CS8604 // Possible null reference argument.
                var following = await _dbContext.Followers
                .Where(x => x.Username == user.Username)
                .ToListAsync();
#pragma warning restore CS8604 // Possible null reference argument.

                List<UserDTO> followingList = new List<UserDTO>();

                if (following.Count() > 0)
                {
                    foreach (var follow in following)
                    {
                        var result = await _dbContext.Users
                            .FirstOrDefaultAsync(x => x.Username == follow.UsernameToFollow);

                        if (result is not null)
                        {
                            followingList.Add(new UserDTO(result.Id, result.Username, result.Name, result.Bio));
                        }
                    }
                }
                return Ok(followingList);
            }
            else
            {
                return BadRequest("No user found with that username");
            }
        }

        [HttpGet("Follow/{userName}/{followUserName}")]
        public async Task<ActionResult> FollowUser(string userName, string followUserName)
        {
            //TODO: Add check if user is authenticated

#pragma warning disable CS8604 // Possible null reference argument.
            var user = await _dbContext.Users
                 .FirstOrDefaultAsync(x => x.Username == followUserName);
#pragma warning restore CS8604 // Possible null reference argument.

#pragma warning disable CS8604 // Possible null reference argument.
            var alreadyFollowing = await _dbContext.Followers
                .AnyAsync(x => x.Username == userName && x.UsernameToFollow == followUserName);
#pragma warning restore CS8604 // Possible null reference argument.

            if (user is not null && !alreadyFollowing)
            {
                var result = await _dbContext.Followers.AddAsync(new Follow(userName, followUserName));
                _dbContext.SaveChanges();

                return Ok();
            }
            else if (alreadyFollowing)
            {
                return BadRequest("You are already following this account");
            }
            else
            {
                return BadRequest("The user you want to follow does not exist");
            }
        }
        [HttpGet("Unfollow/{userName}/{unfollowUserName}")]
        public async Task<ActionResult<IEnumerable<List<UserDTO>>>> UnfollowUser(string userName, string unfollowUserName)
        {
            //TODO: Add check if user is authenticated

#pragma warning disable CS8604 // Possible null reference argument.
            var unfollowRequest = await _dbContext.Followers
                .SingleOrDefaultAsync(x => x.Username == userName && x.UsernameToFollow == unfollowUserName);
#pragma warning restore CS8604 // Possible null reference argument.

            if (unfollowRequest is not null)
            {
                _dbContext.Followers.Remove(unfollowRequest);
                _dbContext.SaveChanges();

                return Ok();
            }
            else
            {
                return BadRequest("You are not following this user");
            }
        }
    }
}

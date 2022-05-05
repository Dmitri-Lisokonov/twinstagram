using AuthenticationService.Util;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shared.DTO.Response;
using Shared.DTO.User;
using Shared.Models.User;
using Shared.Messaging;
using Shared.DTO.RabbitMQ;

namespace AuthenticationService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<AuthenticationUser> _userManager;
        private readonly SignInManager<AuthenticationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<AuthenticationController> _logger;
        private readonly Mapper _mapper;
        private JwtBuilder _jwtBuilder;
        private MessagePublisher _publisher;

        public AuthenticationController(
            ILogger<AuthenticationController> logger,
            UserManager<AuthenticationUser> userManager,
            SignInManager<AuthenticationUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            MessagePublisher publisher,
            IConfiguration config
            )
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _publisher = publisher;
            _jwtBuilder = new JwtBuilder();

            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<RegisterUserDto, AuthenticationUser>();
                cfg.CreateMap<AuthenticationUser, ApplicationUserDto>();
            });

            _mapper = new Mapper(mapperConfig);
      
        }
        
        [HttpGet("test")]
        public async Task<IActionResult> Test()
        {
            _publisher.SendMessage("Hello World!");
            return Ok();
        }

        [HttpPost]
        [Authorize]
        [Route("Register")]
        public async Task<IActionResult> CreateUser([FromBody] RegisterUserDto model)
        {
            if (ModelState.IsValid)
            {
                var user = _mapper.Map<AuthenticationUser>(model);
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "User");
                    return Ok(new ResponseMessage<string>("User created successfully", ResponseStatus.Success.ToString()));
                }
                else
                {
                    return BadRequest(new ResponseMessage<string>("Invalid user input", ResponseStatus.BadRequest.ToString()));
                }
            }
            else
            {
                return BadRequest(new ResponseMessage<string>("Invalid user input", ResponseStatus.BadRequest.ToString()));
            }
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> SignInUser([FromBody] SignInUserDto model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, false, false);
            if (result.Succeeded)
            {
                var user = await _userManager.FindByNameAsync(model.UserName);
                var roles = await _userManager.GetRolesAsync(user);
                var userDto = _mapper.Map<ApplicationUserDto>(user);
                userDto.Token = _jwtBuilder.GenerateToken(user.Id, roles);
                return Ok(new ResponseMessage<ApplicationUserDto>(userDto, ResponseStatus.Success.ToString()));
            }
            else
            {
                return Unauthorized(new ResponseMessage<string>("User unauthorized", ResponseStatus.Unauthorized.ToString()));
            }
        }

        //[Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("Createrole")]
        public async Task<IActionResult> CreateUserRole()
        {
            var role = new IdentityRole();
            role.Name = "User";
            var result = await _roleManager.CreateAsync(role);
            return Ok(result);
        }

        //[Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("Promote/{id}")]
        public async Task<IActionResult> AddUserToRole(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            var result = await _userManager.AddToRoleAsync(user, "User");
            return Ok(result);
        }

        //[Authorize(Roles = "Admin")]
        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> DeleteUser()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (user is not null)
            {
                var result = await _userManager.DeleteAsync(user);
                return Ok(new ResponseMessage<string>("User has been deleted", ResponseStatus.Success.ToString()));
            }
            else
            {
                return NotFound(new ResponseMessage<string>("User not found", ResponseStatus.BadRequest.ToString()));
            }
        }

        [HttpGet]
        [Authorize]
        [Route("Logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok(new ResponseMessage<string>("User logged out", ResponseStatus.Success.ToString()));
        }

        [HttpGet]
        public async Task<IActionResult> IsLoggedIn()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (user is not null)
            {
                return Ok(new ResponseMessage<string>("User is logged in", ResponseStatus.Success.ToString()));
            }
            else
            {
                return Unauthorized(new ResponseMessage<string>("User is not logged in", ResponseStatus.Unauthorized.ToString()));
            }
        }
    }
}
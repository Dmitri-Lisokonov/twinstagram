using AuthenticationService.Models;
using AuthenticationService.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationService.Controllers
{
    [ApiController]
    [Route("api/[controller]/")]
    // test
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<AuthenticationController> _logger;
        private JwtMiddleware _jwtMiddleware;

        public AuthenticationController(
            ILogger<AuthenticationController> logger, 
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration config            
            )
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _jwtMiddleware = new JwtMiddleware(config);
    }

        [HttpPost]
        [Route("create")]
        public async Task<ActionResult> CreateUser([FromBody] RegisterUserModel model)
        {
            var user = new User();
            user.UserName = "HelloIdentity";
            var result = await _userManager.CreateAsync(user, "123Welkom!");
            return Ok(result);
        }
        
        [HttpPost]
        // sign in user
        [Route("login")]
        public async Task<ActionResult> SignInUser([FromBody] SignInUserModel model)
        {
         
            var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, false, false);
            if (result.Succeeded)
            {
                var user = await _userManager.FindByNameAsync(model.UserName);
                var roles = await _userManager.GetRolesAsync(user);
                var token = _jwtMiddleware.GenerateJSONWebToken(roles);
                HttpContext.Response.Headers.Add("Access-Token", token);
            }
            else
            {
                return Unauthorized();
            }
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("createrole")]
        // create a role
        public async Task<ActionResult> CreateUserRole()
        {
            var role = new IdentityRole();
            role.Name = "User";
            var result = await _roleManager.CreateAsync(role);
            return Ok(result);
        }
        
        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("promote/{id}")]
        // add a user to a role
        public async Task<ActionResult> AddUserToRole(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            var result = await _userManager.AddToRoleAsync(user, "User");
            return Ok(result);
        }

        [HttpDelete]
        [Route("delete")]
        public async Task<ActionResult> DeleteUser()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (user is not null)
            {
                var result = await _userManager.DeleteAsync(user);
                return Ok(result);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("logout")]
        public async Task<ActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok();
        }

        // check if user is logged in
        [HttpGet]
        public async Task<ActionResult> IsLoggedIn()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if(user is not null)
            {
                return Ok();
            }
            else
            {
                return Unauthorized();
            }
        }


        [HttpGet]
        [Route("jwt")]
        public ActionResult CheckJwt()
        {
            var results = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role");
            if (results is not null)
            {
                return Ok(results.Value);
            }
            {
                return BadRequest();
            }
        }

    }
}
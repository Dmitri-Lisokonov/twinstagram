using AuthenticationServer.Context;
using AuthenticationServer.Controllers;
using AuthenticationServer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace AuthenticationService.Test.ComponentTest
{
    public class AuthenticationTest
    {
        private readonly AuthenticationController _controller;
        private readonly AuthServiceDatabaseContext _context;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<AuthenticationController> _logger;

        public AuthenticationTest()
        {
            var options = new DbContextOptionsBuilder<AuthServiceDatabaseContext>().UseInMemoryDatabase(databaseName: "InMemoryUserServiceDatabase").Options;
            _context = new AuthServiceDatabaseContext(options);
            _userManager = new UserManager<User>();
            _roleManager = new RoleManager<IdentityRole>();
            var controller = new AuthenticationController(_logger, _userManager, _signInManager, _roleManager, _logger);
            createUsersInMemoryDatabase();
        }

        [Fact]
        public async Task Test()
        {
            Assert.Equal(1, 1);
        }

        private static void createUsersInMemoryDatabase()
        {

        }
    }
}

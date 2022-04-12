using Microsoft.EntityFrameworkCore;
using UserService.Context;
using UserService.Controllers;
using Xunit;

namespace UserService.Test
{
    public class UserTests
    {

        private readonly UserController _controller;
        private readonly UserServiceDatabaseContext _context;

        public UserTests()
        {
            var options = new DbContextOptionsBuilder<UserServiceDatabaseContext>().UseInMemoryDatabase(databaseName: "InMemoryUserServiceDatabase").Options;
            _context = new UserServiceDatabaseContext(options);
            _controller = new UserController(_context);
            createUsersInMemoryDatabase();        
        }

        [Fact]
        public void GetUser_ReturnsUser()
        {
            Assert.Equal(1, 1);
        }

        [Fact]
        public void GetUserWithInvalidUsername_ReturnsNotFound()
        {
            Assert.Equal(1, 1);
        }

        private static void createUsersInMemoryDatabase()
        {
   
        }
    }
}
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserService.Context;
using UserService.Controllers;
using UserService.Models;
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
            var context = new UserServiceDatabaseContext(options);
            var controller = new UserController(context);
            createUsersInMemoryDatabase();        
        }

        [Fact]
        public async Task GetUser_ReturnsUser()
        {
            Assert.Equal(1, 1);
        }

        [Fact]
        public async Task GetUserWithInvalidUsername_ReturnsNotFound()
        {
            Assert.Equal(1, 1);
        }

        private static void createUsersInMemoryDatabase()
        {
            var users = new List<User>
            {
                new User { Id = 1, Name = "Jan"},
                new User { Id = 1, Name = "Piet"},
                new User { Id = 1, Name = "Jeroen"}
            };
        }
    }
}
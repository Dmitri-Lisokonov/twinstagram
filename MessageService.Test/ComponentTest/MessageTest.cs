using MessageService.Context;
using MessageService.Controllers;
using MessageService.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace UserService.Test
{
    public class MessageTest
    {
        private readonly MessageController? _controller;
        private readonly MessageServiceDatabaseContext? _context;

        public MessageTest()
        {
            var options = new DbContextOptionsBuilder<MessageServiceDatabaseContext>().UseInMemoryDatabase(databaseName: "InMemoryUserServiceDatabase").Options;
            var context = new MessageServiceDatabaseContext(options);
            var controller = new MessageController(context);
            createUsersInMemoryDatabase();        
        }

        [Fact]
        public void TestingPipeLine()
        {
            Assert.Equal(1, 1);
        }

        private static void createUsersInMemoryDatabase()
        {

        }
    }
}
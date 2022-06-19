using MessageService.Context;
using MessageService.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shared.Models.Message;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Shared.DTO.Message;
using Shared.DTO.Response;
using Microsoft.AspNetCore.Http;
using Shared.Util;
using Shared.Models.User;
using System.Security.Claims;

namespace UserService.Test
{
    public class MessageTest : IDisposable
    {
        private readonly MessageController _controller;
        private readonly MessageServiceDatabaseContext _context;
        private readonly JwtBuilder _jwtBuilder;
        private static readonly Guid _id = Guid.Parse("4c9379c2-ca69-47d6-97b5-7ef1b5ab926b");
        private static readonly Guid _followId = Guid.Parse("4c9379c2-ca69-47d6-97b5-7ef1b5ab926c");
        private static string _description = "description";
        private static string _image = "image";

        public MessageTest()
        {
            var options = new DbContextOptionsBuilder<MessageServiceDatabaseContext>().UseInMemoryDatabase(databaseName: "InMemoryMessageServiceDatabase").UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking).Options;
            _context = new MessageServiceDatabaseContext(options);
            _controller = new MessageController(_context);
            _controller.ControllerContext.HttpContext = new DefaultHttpContext();
            _jwtBuilder = new JwtBuilder();
            CreateUsersInMemoryDatabase(_context);


            // Create new user
            AuthenticationUser user = new AuthenticationUser
            {
                Id = _id.ToString(),
                UserName = "username",
            };

            var roles = new List<string>
            {
                "user"
            };

            // Generate JWT for authentication
            var token = _jwtBuilder.GenerateToken(user, roles);
            _controller.ControllerContext.HttpContext.Request.Headers["Authorization"] = "Bearer " + token;

            // Add claims to User in HttpContext
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName)
            };

            var userIdentity = new ClaimsIdentity(claims, ClaimTypes.Name);
            _controller.ControllerContext.HttpContext.User = new ClaimsPrincipal(userIdentity);

        }

        [Fact]
        public async Task GetAllMessages()
        {
            var response = await _controller.GetMessages(_id);
            var resultObject = Assert.IsType<OkObjectResult>(response);
            var messages = Assert.IsAssignableFrom<ResponseMessage<IEnumerable<MessageDto>>>(resultObject.Value);
            Assert.Equal(4, messages.Data.Count());
            foreach (var message in messages.Data)
            {
                Assert.Equal(_description, message.Description);
            }
        }

        [Fact]

        public async Task CreateMessage()
        {
            var createdMessage = new CreateMessage
            {
                UserId = Guid.NewGuid(),
                Description = "description",
                Image = "image"
            };

            var response = await _controller.CreateNewMessage(createdMessage);
            var resultObject = Assert.IsType<OkObjectResult>(response);
            var message = Assert.IsAssignableFrom<ResponseMessage<string>>(resultObject.Value);
            Assert.Equal("message created", message.Data);
            Assert.Equal(ResponseStatus.Success.ToString(), message.Status);
        }

        [Fact]
        public async Task GetFeed()
        {
            var response = await _controller.GetFeed();
            var resultObject = Assert.IsType<OkObjectResult>(response);
            var messages = Assert.IsAssignableFrom<ResponseMessage<IEnumerable<MessageDto>>>(resultObject.Value);
            foreach (var message in messages.Data)
            {
                Assert.Equal(_description, message.Description);
                Assert.Equal(_image, message.Image);
                Assert.Equal(_followId, message.UserId);
            }

        }

        private static void CreateUsersInMemoryDatabase(MessageServiceDatabaseContext context)
        {
            var messageData = new List<Message>
            {
                new Message(_id, _description, _image, DateTime.MinValue),
                new Message(_followId, _description, _image, DateTime.MinValue),
                new Message(_id, _description, _image, DateTime.MinValue),
                new Message(_id, _description, _image, DateTime.MinValue)
            };

            var followerData = new List<Follow>
            {
                new Follow(_id, _followId)
            };

            foreach (var follow in followerData)
            {
                context.Followers.Add(follow);
            }

            foreach (var message in messageData)
            {
                context.Messages.Add(message);
            }

            context.SaveChanges();
        }

        public void Dispose() => _context.Dispose();
    }
}
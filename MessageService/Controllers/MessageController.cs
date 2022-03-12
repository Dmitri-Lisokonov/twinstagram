using MessageService.Context;
using Microsoft.AspNetCore.Mvc;


namespace MessageService.Controllers
{
    //TODO: Add try catch handling
    [Route("[controller]")]
    [ApiController]
    public class MessageController : Controller
    {
        private readonly MessageServiceDatabaseContext _dbContext;

        public MessageController(MessageServiceDatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }
    }
}

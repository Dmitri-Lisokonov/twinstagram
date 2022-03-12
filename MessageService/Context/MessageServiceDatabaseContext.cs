using MessageService.Models;
using Microsoft.EntityFrameworkCore;

namespace MessageService.Context
{
    public class MessageServiceDatabaseContext : DbContext
    {

        public MessageServiceDatabaseContext()
        {

        }

        public MessageServiceDatabaseContext(DbContextOptions<MessageServiceDatabaseContext> options) : base(options)
        {

        }

        DbSet<Message> Messages { get; set; };

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }

    }
}

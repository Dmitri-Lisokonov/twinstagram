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

        public DbSet<Message> Messages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Message>().ToTable("Message");


            modelBuilder.Entity<Message>()
                .HasData(
                    new Message() { Id = 1, UserId = 1, Description = "This is a picture of my dog... U like?", Image = "Base64Placeholder", CreatedDate = DateTime.Now  },
                    new Message() { Id = 2, UserId = 2, Description = "I like trains", Image = "Base64Placeholder", CreatedDate = DateTime.Now }
                );
        }

    }
}

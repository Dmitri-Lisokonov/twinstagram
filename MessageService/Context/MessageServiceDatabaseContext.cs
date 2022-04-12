using MessageService.Models;
using Microsoft.EntityFrameworkCore;

namespace MessageService.Context
{
    public class MessageServiceDatabaseContext : DbContext
    {
        public void Increment ()
        {
            int a = 1;
            int b = 2;
            int c = 0;

            c = a + b;
        }
        public void Increment1()
        {
            int a = 1;
            int b = 2;
            int c = 0;

            c = a + b;
        }
        public MessageServiceDatabaseContext()
        {

        }

        public MessageServiceDatabaseContext(DbContextOptions<MessageServiceDatabaseContext> options) : base(options)
        {

        }

        public DbSet<Message>? Messages { get; set; }

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

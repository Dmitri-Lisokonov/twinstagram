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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //Connect to Azure SQL Database if deployed
            if (!optionsBuilder.IsConfigured)
            {
                var connectionString = Environment.GetEnvironmentVariable("db_connection-string");
                if (string.IsNullOrWhiteSpace(connectionString))
                {
                    throw new MissingFieldException("Database environment variable not found.");
                }
                else
                {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                    optionsBuilder.UseSqlServer(Environment.GetEnvironmentVariable("db-connection-string").Replace("DATABASE_NAME", "message-service_db"));
#pragma warning restore CS8602 // Dereference of a possibly null reference.
                }
            }
            
            //Connect to MySQL database for development
            //if (!optionsBuilder.IsConfigured)
            //{
            //    optionsBuilder.UseMySQL("server=localhost;Port=3306;uid=admin;pwd=123Welkom!;database=messageservice_db;");
            //}

            base.OnConfiguring(optionsBuilder);
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

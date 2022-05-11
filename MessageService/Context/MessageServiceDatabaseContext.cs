using Microsoft.EntityFrameworkCore;
using Shared.Models.Message;

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
                    var connectionString = Environment.GetEnvironmentVariable("db-connection-string");
                    if (string.IsNullOrWhiteSpace(connectionString))
                    {
                        throw new MissingFieldException("Database environment variable not found.");
                    }
                    else
                    {
                        optionsBuilder.UseSqlServer(Environment.GetEnvironmentVariable("db-connection-string").Replace("DATABASE_NAME", "message-service_db"));
                    }
            }

            //Connect to MySQL database for development
            //if (!optionsBuilder.IsConfigured)
            //{
            //    optionsBuilder.UseMySQL("server=localhost;Port=3306;uid=admin;pwd=123Welkom!;database=messageservice_db;");
            //}

            //base.OnConfiguring(optionsBuilder);
        }

        public DbSet<Message> Messages { get; set; }
        public DbSet<Like> Likes { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Message>().ToTable("Message");
            
            modelBuilder.Entity<Message>()
                .Property(x => x.Id)
                .ValueGeneratedOnAdd();

        }

    }
}

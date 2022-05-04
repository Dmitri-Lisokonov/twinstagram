using Microsoft.EntityFrameworkCore;
using Shared.Models.User;

namespace UserService.Context
{
    public class UserServiceDatabaseContext : DbContext
    {

        public UserServiceDatabaseContext()
        {

        }

        public UserServiceDatabaseContext(DbContextOptions<UserServiceDatabaseContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //Connect to Azure SQL Database if deployed
//            if (!optionsBuilder.IsConfigured)
//            {
//                var connectionString = Environment.GetEnvironmentVariable("db-connection-string");
//                if (string.IsNullOrWhiteSpace(connectionString))
//                {
//                    throw new MissingFieldException("Database environment variable not found.");
//                }
//                else
//                {
//                    optionsBuilder.UseSqlServer(Environment.GetEnvironmentVariable("db-connection-string").Replace("DATABASE_NAME", "message-service_db"));
//                }
//            }
            
            //Connect to MySQL database for development
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySQL("server=localhost;Port=3306;uid=admin;pwd=123Welkom!;database=userservice_db;");
            }

            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<ApplicationUser> Users { get; set; }
        public DbSet<Follow> Followers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicationUser>().ToTable("User");
            modelBuilder.Entity<Follow>().ToTable("Follower");

            modelBuilder.Entity<ApplicationUser>()
                .HasData(
                    new ApplicationUser("username", "name", "bio", "base64string"),
                    new ApplicationUser("username2", "name2", "bio2", "base64string2")
                );

            modelBuilder.Entity<ApplicationUser>()
            .Property(f => f.Id)
            .ValueGeneratedOnAdd();
        }

    }
}

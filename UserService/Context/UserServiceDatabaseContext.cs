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
            if (!optionsBuilder.IsConfigured)
            {
                var connectionString = Environment.GetEnvironmentVariable("db-connection-string");
                if (string.IsNullOrWhiteSpace(connectionString))
                {
                    throw new MissingFieldException("Database environment variable not found.");
                }
                else
                {
                    optionsBuilder.UseSqlServer(Environment.GetEnvironmentVariable("db-connection-string").Replace("DATABASE_NAME", "user-service_db"));
                }
            }

            // Connect to MySQL database for development
            //if (!optionsBuilder.IsConfigured)
            //    {
            //        optionsBuilder.UseMySQL("server=localhost;Port=3306;uid=admin;pwd=123Welkom!;database=userservice_db;");
            //    }

            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<ApplicationUser> Users { get; set; }
        public DbSet<Follow> Followers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicationUser>()
                .Property(f => f.Id)
                .ValueGeneratedOnAdd();
            
            modelBuilder.Entity<Follow>()
                .Property(f => f.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<ApplicationUser>()
                .Property(f => f.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<ApplicationUser>()
                .HasIndex(x => x.Username)
                .IsUnique();
        }
    }
}

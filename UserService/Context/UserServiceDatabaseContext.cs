using Microsoft.EntityFrameworkCore;
using UserService.Models;

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

        public DbSet<User>? Users { get; set; }
        public DbSet<Follow>? Followers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<Follow>().ToTable("Follower");

            modelBuilder.Entity<User>()
                .HasData(
                    new User(1, "Jan", "jan_verkouden", "altijd ziek...")
                );

            modelBuilder.Entity<Follow>()
              .HasData(
                  new Follow(1, "jan_verkouden", "lucas_leip") 
              );
        }

    }
}

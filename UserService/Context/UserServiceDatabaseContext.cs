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

        public DbSet<ApplicationUser>? Users { get; set; }
        public DbSet<Follow>? Followers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicationUser>().ToTable("User");
            modelBuilder.Entity<Follow>().ToTable("Follower");

            modelBuilder.Entity<ApplicationUser>()
            .Property(f => f.Id)
            .ValueGeneratedOnAdd();


            modelBuilder.Entity<ApplicationUser>()
                .HasData(
                    new ApplicationUser()
                    {
                        Id = 1,
                        Username = "a",
                        Name = "Jan",
                        Bio = "I am a programmer",
                    },
                    new ApplicationUser()
                    {
                        Id = 2,
                        Username = "b",
                        Name = "Piet",
                        Bio = "I am a chef",
                    }
                );

            modelBuilder.Entity<Follow>()
              .HasData(
                  new Follow()
                  {
                      Id = 1,
                      Username = "a",
                      UsernameToFollow = "b"
                  }
              );
        }

    }
}

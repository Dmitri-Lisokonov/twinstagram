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

        public DbSet<User> Users { get; set; }
        public DbSet<Follow> Followers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<Follow>().ToTable("Follower");

            modelBuilder.Entity<User>()
                .HasData(
                    new User() { Id = 1, Name = "Jan", Username = "jan_verkouden", Bio = "altijd ziek...", Password = "Test", PasswordSalt = "Salty" },
                    new User() { Id = 2, Name = "Klaas", Username = "klaas_debaas", Bio = "big boss", Password = "Test", PasswordSalt = "Salty" },
                    new User() { Id = 3, Name = "Lucas", Username = "lucas_leip", Bio = "leipe vent", Password = "Test", PasswordSalt = "Salty" }
                );

            modelBuilder.Entity<Follow>()
              .HasData(
                  new Follow() { Id = 1, Username = "jan_verkouden", UsernameToFollow = "lucas_leip" },
                  new Follow() { Id = 2, Username = "jan_verkouden", UsernameToFollow = "klaas_debaas" },
                  new Follow() { Id = 3, Username = "klaas_debaas", UsernameToFollow = "jan_verkouden" },
                  new Follow() { Id = 4, Username = "lucas_leip", UsernameToFollow = "jan_verkouden" }
              );
        }

    }
}

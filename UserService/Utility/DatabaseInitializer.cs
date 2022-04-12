using UserService.Context;

namespace UserService.Utility
{
    public class DatabaseInitializer
    {
        public static void Initialize(UserServiceDatabaseContext context)
        {
            context.Database.EnsureCreated();

#pragma warning disable CS8604 // Possible null reference argument.
            if (context.Users.Any())
            {
                return;
            }
#pragma warning restore CS8604 // Possible null reference argument.
        }

        public static void DeleteAfter(UserServiceDatabaseContext context)
        {
            context.Database.EnsureDeleted();
        }

    }
}

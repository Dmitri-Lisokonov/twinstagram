using UserService.Models;

namespace UserService.Utility
{
    public class DatabaseInitializer
    {
        public static void Initialize(Context.UserServiceDatabaseContext context)
        {
            context.Database.EnsureCreated();

            if (context.Users.Any())
            {
                return;
            }
        }

        public static void DeleteAfter(Context.UserServiceDatabaseContext context)
        {
            context.Database.EnsureDeleted();
        }

    }
}

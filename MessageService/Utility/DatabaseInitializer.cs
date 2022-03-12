using MessageService.Context;
using UserService.Models;

namespace UserService.Utility
{
    public class DatabaseInitializer
    {
        public static void Initialize(MessageServiceDatabaseContext context)
        {
            context.Database.EnsureCreated();

            if (context.Messages.Any())
            {
                return;
            }
        }

        public static void DeleteAfter(MessageServiceDatabaseContext context)
        {
            context.Database.EnsureDeleted();
        }

    }
}

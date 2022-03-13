using MessageService.Context;
<<<<<<< HEAD
using UserService.Models;

namespace UserService.Utility
=======

namespace MessageService.Utility
>>>>>>> e884d9240d91d31b7bb0866a701fb38791c9aa11
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

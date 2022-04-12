using MessageService.Context;

namespace MessageService.Utility
{
    public class DatabaseInitializer
    {
        public static void Initialize(MessageServiceDatabaseContext context)
        {
            context.Database.EnsureCreated();

#pragma warning disable CS8604 // Possible null reference argument.
            if (context.Messages.Any())
            {
                return;
            }
#pragma warning restore CS8604 // Possible null reference argument.
        }

        public static void DeleteAfter(MessageServiceDatabaseContext context)
        {
            context.Database.EnsureDeleted();
        }

    }
}

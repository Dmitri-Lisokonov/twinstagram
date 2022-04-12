﻿using UserService.Context;

namespace UserService.Utility
{
    public class DatabaseInitializer
    {
        public static void Initialize(UserServiceDatabaseContext context)
        {
            context.Database.EnsureCreated();

            if (context.Users.Any())
            {
                return;
            }
        }

        public static void DeleteAfter(UserServiceDatabaseContext context)
        {
            context.Database.EnsureDeleted();
        }

    }
}
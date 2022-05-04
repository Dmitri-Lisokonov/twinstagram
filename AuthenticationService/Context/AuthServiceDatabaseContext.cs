﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Shared.Models;
using Shared.Models.User;

namespace AuthenticationService.Context
{
    public class AuthServiceDatabaseContext : IdentityDbContext<AuthenticationUser>
    {

        public AuthServiceDatabaseContext()
        {

        }

        public AuthServiceDatabaseContext(DbContextOptions<AuthServiceDatabaseContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //Connect to Azure SQL Database if deployed
//            if (!optionsBuilder.IsConfigured)
//            {
//                var connectionString = Environment.GetEnvironmentVariable("db-connection-string");
//                if (string.IsNullOrWhiteSpace(connectionString))
//                {
//                    throw new MissingFieldException("Database environment variable not found.");
//                }
//                else
//                {
//#pragma warning disable CS8602 // Dereference of a possibly null reference.
//                    optionsBuilder.UseSqlServer(Environment.GetEnvironmentVariable("db-connection-string").Replace("DATABASE_NAME", "message-service_db"));
//#pragma warning restore CS8602 // Dereference of a possibly null reference.
//                }
//            }
            
            //Connect to MySQL database for development
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql("server=localhost;Port=3306;uid=admin;pwd=123Welkom!;database=authentication;", new MySqlServerVersion(new Version(8, 0, 28)));
            }

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

    }
}

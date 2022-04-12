﻿using AuthenticationService.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationService.Context
{
    public class AuthServiceDatabaseContext : IdentityDbContext<User>
    {

        public AuthServiceDatabaseContext()
        {

        }

        public AuthServiceDatabaseContext(DbContextOptions<AuthServiceDatabaseContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

    }
}
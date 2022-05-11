﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
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
            if (!optionsBuilder.IsConfigured)
            {
                var connectionString = Environment.GetEnvironmentVariable("db-connection-string");
                if (string.IsNullOrWhiteSpace(connectionString))
                {
                    throw new MissingFieldException("Database environment variable not found.");
                }
                else
                {
                    optionsBuilder.UseSqlServer(Environment.GetEnvironmentVariable("db-connection-string").Replace("DATABASE_NAME", "authentication-service_db"));

                }
            }

            //Connect to MySQL database for development
            //if (!optionsBuilder.IsConfigured)
            //{
            //    optionsBuilder.UseMySql("server=localhost;Port=3306;uid=admin;pwd=123Welkom!;database=authentication;", new MySqlServerVersion(new Version(8, 0, 28)));
            //}

            //base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder
               .HasAnnotation("ProductVersion", "6.0.4")
               .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
            {
                b.Property<string>("Id")
                    .HasColumnType("varchar(255)");

                b.Property<string>("ConcurrencyStamp")
                    .IsConcurrencyToken()
                    .HasColumnType("longtext");

                b.Property<string>("Name")
                    .HasMaxLength(256)
                    .HasColumnType("varchar(256)");

                b.Property<string>("NormalizedName")
                    .HasMaxLength(256)
                    .HasColumnType("varchar(256)");

                b.HasKey("Id");

                b.HasIndex("NormalizedName")
                    .IsUnique()
                    .HasDatabaseName("RoleNameIndex");

                b.ToTable("AspNetRoles", (string)null);
            });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int");

                b.Property<string>("ClaimType")
                    .HasColumnType("longtext");

                b.Property<string>("ClaimValue")
                    .HasColumnType("longtext");

                b.Property<string>("RoleId")
                    .IsRequired()
                    .HasColumnType("varchar(255)");

                b.HasKey("Id");

                b.HasIndex("RoleId");

                b.ToTable("AspNetRoleClaims", (string)null);
            });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int");

                b.Property<string>("ClaimType")
                    .HasColumnType("longtext");

                b.Property<string>("ClaimValue")
                    .HasColumnType("longtext");

                b.Property<string>("UserId")
                    .IsRequired()
                    .HasColumnType("varchar(255)");

                b.HasKey("Id");

                b.HasIndex("UserId");

                b.ToTable("AspNetUserClaims", (string)null);
            });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
            {
                b.Property<string>("LoginProvider")
                    .HasColumnType("varchar(255)");

                b.Property<string>("ProviderKey")
                    .HasColumnType("varchar(255)");

                b.Property<string>("ProviderDisplayName")
                    .HasColumnType("longtext");

                b.Property<string>("UserId")
                    .IsRequired()
                    .HasColumnType("varchar(255)");

                b.HasKey("LoginProvider", "ProviderKey");

                b.HasIndex("UserId");

                b.ToTable("AspNetUserLogins", (string)null);
            });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
            {
                b.Property<string>("UserId")
                    .HasColumnType("varchar(255)");

                b.Property<string>("RoleId")
                    .HasColumnType("varchar(255)");

                b.HasKey("UserId", "RoleId");

                b.HasIndex("RoleId");

                b.ToTable("AspNetUserRoles", (string)null);
            });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
            {
                b.Property<string>("UserId")
                    .HasColumnType("varchar(255)");

                b.Property<string>("LoginProvider")
                    .HasColumnType("varchar(255)");

                b.Property<string>("Name")
                    .HasColumnType("varchar(255)");

                b.Property<string>("Value")
                    .HasColumnType("longtext");

                b.HasKey("UserId", "LoginProvider", "Name");

                b.ToTable("AspNetUserTokens", (string)null);
            });

            modelBuilder.Entity("Shared.Models.User.AuthenticationUser", b =>
            {
                b.Property<string>("Id")
                    .HasColumnType("varchar(255)");

                b.Property<int>("AccessFailedCount")
                    .HasColumnType("int");

                b.Property<string>("ConcurrencyStamp")
                    .IsConcurrencyToken()
                    .HasColumnType("longtext");

                b.Property<string>("Email")
                    .HasMaxLength(256)
                    .HasColumnType("varchar(256)");

                b.Property<bool>("EmailConfirmed")
                    .HasColumnType("tinyint(1)");

                b.Property<bool>("LockoutEnabled")
                    .HasColumnType("tinyint(1)");

                b.Property<DateTimeOffset?>("LockoutEnd")
                    .HasColumnType("datetime(6)");

                b.Property<string>("Name")
                    .HasColumnType("longtext");

                b.Property<string>("NormalizedEmail")
                    .HasMaxLength(256)
                    .HasColumnType("varchar(256)");

                b.Property<string>("NormalizedUserName")
                    .HasMaxLength(256)
                    .HasColumnType("varchar(256)");

                b.Property<string>("PasswordHash")
                    .HasColumnType("longtext");

                b.Property<string>("PhoneNumber")
                    .HasColumnType("longtext");

                b.Property<bool>("PhoneNumberConfirmed")
                    .HasColumnType("tinyint(1)");

                b.Property<string>("SecurityStamp")
                    .HasColumnType("longtext");

                b.Property<bool>("TwoFactorEnabled")
                    .HasColumnType("tinyint(1)");

                b.Property<string>("UserName")
                    .HasMaxLength(256)
                    .HasColumnType("varchar(256)");

                b.HasKey("Id");

                b.HasIndex("NormalizedEmail")
                    .HasDatabaseName("EmailIndex");

                b.HasIndex("NormalizedUserName")
                    .IsUnique()
                    .HasDatabaseName("UserNameIndex");

                b.ToTable("AspNetUsers", (string)null);
            });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
            {
                b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                    .WithMany()
                    .HasForeignKey("RoleId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();
            });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
            {
                b.HasOne("Shared.Models.User.AuthenticationUser", null)
                    .WithMany()
                    .HasForeignKey("UserId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();
            });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
            {
                b.HasOne("Shared.Models.User.AuthenticationUser", null)
                    .WithMany()
                    .HasForeignKey("UserId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();
            });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
            {
                b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                    .WithMany()
                    .HasForeignKey("RoleId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.HasOne("Shared.Models.User.AuthenticationUser", null)
                    .WithMany()
                    .HasForeignKey("UserId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();
            });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
            {
                b.HasOne("Shared.Models.User.AuthenticationUser", null)
                    .WithMany()
                    .HasForeignKey("UserId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();
            });

        }

    }
}

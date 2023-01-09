using ECommerce.Domain.Entities.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Domain.Infrastructure.Contexts
{
    public sealed partial class DatabaseContext : DbContext
    {
        public DatabaseContext()
        {

        }
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<RoleAuthorization> RoleAuthorizations { get; set; }
        public DbSet<Authorization> Auhtorizations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // indexler
            modelBuilder.Entity<User>().HasIndex(p => new { p.Guid }).IsUnique();
            modelBuilder.Entity<Role>().HasIndex(p => new { p.Guid }).IsUnique();
            modelBuilder.Entity<RoleAuthorization>().HasIndex(p => new { p.Guid }).IsUnique();
            modelBuilder.Entity<Authorization>().HasIndex(p => new { p.Guid }).IsUnique();
            modelBuilder.Entity<UserRole>().HasIndex(p => new { p.Guid }).IsUnique();


            modelBuilder.Entity<User>()
                .HasIndex(p => new { p.UserName, p.Deleted }).IsUnique().HasFilter(null);

            modelBuilder.Entity<UserRole>()
               .HasIndex(p => new { p.UserId, p.RoleId, p.Deleted }).IsUnique().HasFilter(null);

            modelBuilder.Entity<Authorization>()
                .HasIndex(p => new { p.Name, p.Deleted }).IsUnique().HasFilter(null);

            modelBuilder.Entity<RoleAuthorization>()
                .HasIndex(p => new { p.RoleId, p.AuthorizationId, p.Deleted }).IsUnique().HasFilter(null);

            modelBuilder.Entity<Role>()
                .HasIndex(p => new { p.Name, p.Deleted }).IsUnique().HasFilter(null);


        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();
            optionsBuilder.UseNpgsql(configuration.GetConnectionString("PostgreSQL"));
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true); //postegrosql time span hatası
            AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
        }
    }
}

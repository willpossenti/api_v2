using Ecosistemas.API.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecosistemas.API.Data
{
    public class CatalogoDbContext : DbContext
    {
        public CatalogoDbContext(
            DbContextOptions<CatalogoDbContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }

        public DbSet<Log> Logs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
            .HasKey(t => t.UserId);

            modelBuilder.Entity<Role>()
            .HasKey(t => t.RoleId);

            modelBuilder.Entity<UserRole>()
            .HasKey(t => t.UserRoleId);

            modelBuilder.Entity<Log>()
            .HasKey(t => t.LogId);


            modelBuilder.Entity<UserRole>()
                .HasOne(pt => pt.Role)
                .WithMany(p => p.UserRoles);

            modelBuilder.Entity<UserRole>()
                .HasOne(pt => pt.User)
                .WithMany(t => t.UserRoles);

            modelBuilder.Entity<Log>()
                .HasOne(pt => pt.User);
        }
    }
}

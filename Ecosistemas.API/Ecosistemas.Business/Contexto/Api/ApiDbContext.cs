using Ecosistemas.Business.Entities.Api;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecosistemas.Business.Contexto.Api
{
    public class ApiDbContext : DbContext
    {
        public ApiDbContext(
            DbContextOptions<ApiDbContext> options) : base(options)
        {

        }

        public ApiDbContext()
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Log> Logs { get; set; }
        public DbSet<Acesso> Acessos { get; set; }
        public DbSet<Sistema> Sistemas { get; set; }
        public DbSet<Unidade> Unidades { get; set; }
        public DbSet<Cliente> Clientes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           // modelBuilder.Entity<User>()
           // .HasKey(t => t.UserId);

           // modelBuilder.Entity<Role>()
           // .HasKey(t => t.RoleId);

           // modelBuilder.Entity<UserRole>()
           // .HasKey(t => t.UserRoleId);

           // modelBuilder.Entity<Log>()
           // .HasKey(t => t.LogId);

           // modelBuilder.Entity<Acesso>()
           // .HasKey(t => t.AcessoId);

           // modelBuilder.Entity<UserRole>()
           //     .HasOne(pt => pt.Role)
           //     .WithMany(p => p.UserRoles);

           // modelBuilder.Entity<UserRole>()
           //     .HasOne(pt => pt.User)
           //     .WithMany(t => t.UserRoles);

           // modelBuilder.Entity<Log>()
           //     .HasOne(pt => pt.User);

           // modelBuilder.Entity<Acesso>()
           //.HasOne(pt => pt.User);
        }
    }
}

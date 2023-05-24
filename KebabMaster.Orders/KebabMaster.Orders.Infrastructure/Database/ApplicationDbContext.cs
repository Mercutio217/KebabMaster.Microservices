using KebabMaster.Orders.Domain;
using KebabMaster.Orders.Domain.DTOs.Authorization;
using KebabMaster.Orders.Domain.Entities;
using KebabMaster.Orders.Infrastructure.DTOs.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;

namespace KebabMaster.Orders.Infrastructure.Database;

public class ApplicationDbContext : DbContext
{
    public DbSet<Order> Orders { get; set; }
    public DbSet<User> Users { get; set; }
    
    public DbSet<Role> Roles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=OrderDatabase");    
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasKey(key => key.Email);
        
        modelBuilder.Entity<User>()
            .HasMany(ent => ent.Roles)
            .WithMany();
        
        modelBuilder.Entity<User>()
            .HasIndex(u => u.UserName)
            .IsUnique();

        modelBuilder.Entity<Role>()
            .HasKey(role => new { role.Id, role.Name });
        
        base.OnModelCreating(modelBuilder);
    }
}
using KebabMaster.Orders.Domain;
using KebabMaster.Orders.Domain.DTOs.Authorization;
using KebabMaster.Orders.Domain.Entities;
using KebabMaster.Orders.Infrastructure.DTOs.Authorization;
using KebabMaster.Orders.Infrastructure.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.Extensions.Options;

namespace KebabMaster.Orders.Infrastructure.Database;

public class ApplicationDbContext : DbContext
{
    private readonly DatabaseOptions _databaseOptions;
    public ApplicationDbContext(IOptions<DatabaseOptions> options)
    {
        _databaseOptions = options.Value;
    }
    public DbSet<Order> Orders { get; set; }
    public DbSet<MenuItem> MenuItems { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(_databaseOptions.ConnectionString);    
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
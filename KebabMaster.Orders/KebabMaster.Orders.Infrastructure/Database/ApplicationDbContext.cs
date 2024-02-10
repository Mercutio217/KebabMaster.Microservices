using KebabMaster.Orders.Domain;
using KebabMaster.Orders.Domain.Entities;
using KebabMaster.Orders.Infrastructure.Settings;
using Microsoft.EntityFrameworkCore;
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
        // optionsBuilder.UseSqlServer(_databaseOptions.ConnectionString);    
        optionsBuilder.UseInMemoryDatabase("tfuj stary");    

    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasKey(key => key.Id);

        modelBuilder.Entity<User>()
            .Property(us => us.Email)
            .HasMaxLength(50);
        
        modelBuilder.Entity<User>()
            .Property(us => us.Name)
            .HasMaxLength(50);
        
        modelBuilder.Entity<User>()
            .Property(us => us.Surname)
            .HasMaxLength(50);
        
        modelBuilder.Entity<User>()
            .Property(us => us.UserName)
            .HasMaxLength(50);
        
        modelBuilder.Entity<User>()
            .HasMany(ent => ent.Roles)
            .WithMany();
        
        modelBuilder.Entity<User>()
            .HasIndex(u => u.UserName)
            .IsUnique();

        modelBuilder.Entity<Role>()
            .HasKey(role => role.Id);

        modelBuilder.Entity<Role>()
            .HasIndex(role => role.Name)
            .IsUnique();

        modelBuilder.Entity<Role>()
            .Property(r => r.Name)
            .HasMaxLength(25);
        
        base.OnModelCreating(modelBuilder);
    }
}
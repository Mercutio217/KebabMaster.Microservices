using KebabMaster.Orders.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace KebabMaster.Orders.Infrastructure.Tests.Mocks;

public class MockDbContext : ApplicationDbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase("OrdersDb");    
    }

}
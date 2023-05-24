using KebabMaster.Orders.Domain.Entities.Base;

namespace KebabMaster.Orders.Domain;

public class MenuItem : Entity
{
    public string Name { get; }
    public double Price { get; }
    
    private MenuItem() { }
}
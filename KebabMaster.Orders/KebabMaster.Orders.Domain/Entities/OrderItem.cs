using KebabMaster.Orders.Domain.Entities.Base;

namespace KebabMaster.Orders.Domain.Entities;

public class OrderItem : Entity
{
    public Guid MenuItemId { get; set; }
    public int Quantity { get; set; }
    
    private OrderItem() { }

    private OrderItem(Guid menuItemId, int quantity)
    {
        MenuItemId = menuItemId;
        Quantity = quantity;
    }

    public static OrderItem Create(Guid menuItemId, int quantity)
    {
        if (menuItemId == default || quantity < 1)
            throw new Exception();

        return new OrderItem(menuItemId, quantity);
    }
}
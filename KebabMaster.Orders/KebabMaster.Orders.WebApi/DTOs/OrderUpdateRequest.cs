using KebabMaster.Orders.Services;

namespace KebabMaster.Orders.DTOs;

public class OrderUpdateRequest
{
    public Guid Id { get; set; }
    public AddressDto Address { get; set; }
    public IEnumerable<OrderItemDto> OrderItems { get; set; }
}
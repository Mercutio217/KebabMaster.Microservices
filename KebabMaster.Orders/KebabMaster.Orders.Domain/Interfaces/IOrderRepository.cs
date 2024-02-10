using KebabMaster.Orders.Domain.DTOs;
using KebabMaster.Orders.Domain.Entities;
using KebabMaster.Orders.Domain.Entities.Base;
using KebabMaster.Orders.Domain.Filters;

namespace KebabMaster.Orders.Domain.Interfaces;

public interface IOrderRepository
{
    Task CreateOrder(Order order);
    Task<IEnumerable<Order>> GetOrdersAsync(OrderFilter filter);
    Task<Order> GetOrderById(Guid id);
    Task DeleteOrder(Guid id);
    Task UpdateOrder(OrderUpdateModel order);
    Task<IEnumerable<Order>> GetOrdersByUserId(Guid id);
}
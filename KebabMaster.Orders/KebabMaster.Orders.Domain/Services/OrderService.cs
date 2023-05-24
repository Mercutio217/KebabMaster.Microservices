using KebabMaster.Orders.Domain.DTOs;
using KebabMaster.Orders.Domain.Entities;
using KebabMaster.Orders.Domain.Entities.Base;
using KebabMaster.Orders.Domain.Exceptions;
using KebabMaster.Orders.Domain.Interfaces;

namespace KebabMaster.Orders.Domain.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _repository;

    public OrderService(IOrderRepository repository)
    {
        _repository = repository;
    }

    public Task CreateOrder(Order order)
    {
        return _repository.CreateOrder(order);
    }

    public Task<IEnumerable<Order>> GetOrdersAsync(OrderFilter filter)
    {
        return _repository.GetOrdersAsync(filter);
    }

    public Task<Order> GetOrderByIdAsync(Guid id)
    {
        return _repository.GetOrderById(id);
    }

    public Task DeleteOrder(Guid id)
    {
        return _repository.DeleteOrder(id);
    }

    public Task UpdateOrder(OrderUpdateModel order)
    {
        return _repository.UpdateOrder(order);
    }
}
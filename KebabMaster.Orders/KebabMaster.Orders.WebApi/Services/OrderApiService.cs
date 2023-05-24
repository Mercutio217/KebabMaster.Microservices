using System.ComponentModel.DataAnnotations;
using AutoMapper;
using KebabMaster.Orders.Domain;
using KebabMaster.Orders.Domain.DTOs;
using KebabMaster.Orders.Domain.Entities;
using KebabMaster.Orders.Domain.Entities.Base;
using KebabMaster.Orders.Domain.Exceptions;
using KebabMaster.Orders.Domain.Interfaces;
using KebabMaster.Orders.DTOs;

namespace KebabMaster.Orders.Services;

public class OrderApiService : IOrderApiService
{
    private readonly IOrderService _orderService;
    private readonly IMapper _mapper;
    private readonly IApplicationLogger _logger;

    public OrderApiService(
        IOrderService orderService,
        IMapper mapper,
        IApplicationLogger logger)
    {
        _orderService = orderService;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task CreateOrder(OrderRequest order)
    {
        await Execute(async () =>
        {
            _logger.LogPostStart(order);
            await _orderService.CreateOrder(_mapper.Map<Order>(order));

            _logger.LogPostEnd(order);
        });
    }

    public async Task<ApplicationResponse<OrderResponse>> GetOrdersAsync(OrderFilter filter) =>
        await Execute(async () =>
        {
            _logger.LogGetStart(filter);
            IEnumerable<Order> result = await _orderService.GetOrdersAsync(filter);
            _logger.LogGetEnd(filter);
            return new ApplicationResponse<OrderResponse>(
                _mapper.Map<IEnumerable<OrderResponse>>(result));
        });

    public async Task<OrderResponse> GetOrderById(Guid id) =>
        await Execute(async () =>
        {
            _logger.LogGetStart(id);
            Order result = await Execute<Order>(() => _orderService.GetOrderByIdAsync(id));
            _logger.LogGetEnd(id);

            return _mapper.Map<OrderResponse>(result);
        });

    public async Task DeleteOrder(Guid id) =>
        await Execute(async () =>
        {
            _logger.LogDeleteStart(id);
            await _orderService.DeleteOrder(id);
            _logger.LogDeleteEnd(id);
        });

    public async Task UpdateOrder(OrderUpdateRequest order) =>
        await Execute(async () =>
        {
            _logger.LogDeleteStart(order);
            var dto = _mapper.Map<OrderUpdateModel>(order);

            await _orderService.UpdateOrder(dto);

            _logger.LogDeleteEnd(order);
        });

    private async Task Execute(Func<Task> function)
    {
        try
        {
            await function();
        }
        catch (ApplicationValidationException validationException)
        {
            _logger.LogValidationException(validationException);
        }
        catch (Exception exception)
        {
            _logger.LogException(exception);
        }
    }

    private async Task<T> Execute<T>(Func<Task<T>> function)
    {
        try
        {
            return await function();
        }
        catch (ApplicationValidationException validationException)
        {
            _logger.LogValidationException(validationException);
            throw;
        }
        catch (Exception exception)
        {
            _logger.LogException(exception);
            throw;
        }
    }
}

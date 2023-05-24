using System.Data;
using KebabMaster.Orders.Domain.DTOs.Authorization;
using KebabMaster.Orders.Domain.Interfaces;
using KebabMaster.Orders.DTOs;
using KebabMaster.Orders.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KebabMaster.Orders.Controllers;

[ApiController]
[Route("[controller]")]
public class OrdersController : ApplicationBaseController
{
    private IOrderApiService _orderApiService;
    
    public OrdersController(
        IOrderApiService orderApiService)
    {
        _orderApiService = orderApiService;
    }
    
    /// <summary>
    /// Getting already created orders by filter
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    [Authorize]
    [HttpGet]
    public async Task<ActionResult<ApplicationResponse<OrderResponse>>> Get([FromQuery] OrderFilter filter)
    {
        return await Execute(() => _orderApiService.GetOrdersAsync(filter));
    }
    /// <summary>
    /// Getting already created orders by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [Authorize]
    [HttpGet("{id}")]
    public async Task<ActionResult<OrderResponse>> Get(Guid id) => 
        await Execute(() => _orderApiService.GetOrderById(id));
    /// <summary>
    /// Creating new order
    /// </summary>
    /// <param name="orderRequest"></param>
    /// <returns></returns>
    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Post(OrderRequest orderRequest) => 
        await Execute(() => _orderApiService.CreateOrder(orderRequest), NoContent());
    /// <summary>
    /// Updating order
    /// </summary>
    /// <param name="updateRequest"></param>
    /// <returns></returns>
    [Authorize(Roles = "Admin")]
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(OrderUpdateRequest updateRequest) => 
        await Execute(() => _orderApiService.UpdateOrder(updateRequest), NoContent());

    /// <summary>
    /// Deleting order
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id) => 
        await Execute(() => _orderApiService.DeleteOrder(id), NoContent());

}
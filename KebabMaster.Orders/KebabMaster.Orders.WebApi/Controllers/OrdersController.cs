using System.Data;
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

    // GET: api/OrderRequests
    // [Authorize(Roles = "Admin")]
    [HttpGet]
    public async Task<ActionResult<ApplicationResponse<OrderResponse>>> Get([FromQuery] OrderFilter filter)
    {
        return await Execute(() => _orderApiService.GetOrdersAsync(filter));
    }

    // GET: api/OrderRequests/5
    [HttpGet("{id}")]
    public async Task<ActionResult<OrderResponse>> Get(Guid id) => 
        await Execute(() => _orderApiService.GetOrderById(id));

    // POST: api/OrderRequests
    [HttpPost]
    public async Task<IActionResult> Post(OrderRequest orderRequest) => 
        await Execute(() => _orderApiService.CreateOrder(orderRequest), NoContent());

    // PUT: api/OrderRequests/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(OrderUpdateRequest updateRequest) => 
        await Execute(() => _orderApiService.UpdateOrder(updateRequest), NoContent());

    // DELETE: api/OrderRequests/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id) => 
        await Execute(() => _orderApiService.DeleteOrder(id), NoContent());

}
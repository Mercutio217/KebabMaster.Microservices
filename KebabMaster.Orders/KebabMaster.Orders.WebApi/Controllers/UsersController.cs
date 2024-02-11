using KebabMaster.Orders.Domain.Entities;
using KebabMaster.Orders.Domain.Interfaces;
using KebabMaster.Orders.DTOs;
using KebabMaster.Orders.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KebabMaster.Orders.Controllers;

[Route("[controller]")]
public class UsersController : ApplicationBaseController
{
    private readonly IUserManagementService _usersService;
    private readonly IOrderApiService _ordersService;

    public UsersController(IUserManagementService usersService, 
        IOrderApiService ordersService)
    {
        _usersService = usersService;
        _ordersService = ordersService;
    }
    
    [HttpGet]
    [Authorize]
    public async Task<ActionResult<IEnumerable<UserResponse>>> Get(
        [FromQuery] UserRequest model
    )
    {
        return await Execute(() => _usersService.GetByFilter(model));
    }
    
    [HttpGet("{id}")]
    [Authorize]
    public async Task<ActionResult<UserResponse>> GetById(
        [FromQuery] Guid id
    )
    {
        return await Execute(() => _usersService.GetById(id));
    }
    
    [HttpDelete]
    [Authorize(Roles = "Admin")]
    [Route("{id}")]
    public async Task<IActionResult> Delete(
        [FromRoute] Guid id
    )
    {
        return await Execute(() => _usersService.DeleteUser(id), Ok());
    }
    
    [HttpPut]
    public async Task<IActionResult> Update(
        [FromBody] UserUpdateRequest request
    )
    {
        return await Execute(() => _usersService.UpdateUser(request), Ok());
    }
    
    [HttpGet("{id}/orders")]
    public async Task<ActionResult<IEnumerable<Order>>> GetOrdersById(
        [FromRoute] Guid id
    )
    {
        return await Execute(() => _ordersService.GetOrdersByUserId(id));
    }
    
    [HttpPost("{id}/orders")]
    public async Task<IActionResult> CreateOrdersForUser(
        [FromBody] OrderUserRequest request
    )
    {
        return await Execute(() => _ordersService.CreateUserOrders(request), Ok());
    }
}
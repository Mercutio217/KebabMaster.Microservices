using KebabMaster.Authorization.DTOs;
using KebabMaster.Authorization.Interfaces;
using KebabMaster.Orders.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KebabMaster.Authorization.Controllers;

[Route("[controller]")]
public class UsersController : ApplicationBaseController
{
    private readonly IUserManagementService _service;

    public UsersController(IUserManagementService service)
    {
        _service = service;
    }
    
    [HttpGet]
    [Authorize(Roles = "Admin")]
    [Route("users")]
    public async Task<ActionResult<IEnumerable<UserResponse>>> Get(
        [FromQuery] UserRequest model
    )
    {
        return await Execute(() => _service.GetByFilter(model));
    }
    
    [HttpDelete]
    [Authorize(Roles = "Admin")]
    [Route("users/{email}")]
    public async Task<IActionResult> Delete(
        [FromRoute] string email
    )
    {
        return await Execute(() => _service.DeleteUser(email), Ok());
    }
    
    [HttpDelete]
    [Route("users/{email}")]
    public async Task<IActionResult> Update(
        [FromRoute] string email
    )
    {
        return await Execute(() => _service.dz(email), Ok());
    }
    
}
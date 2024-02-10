using KebabMaster.Orders.DTOs;
using KebabMaster.Orders.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KebabMaster.Orders.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthorizationController : ApplicationBaseController
{
    private readonly IUserManagementService _service;

    public AuthorizationController(IUserManagementService service)
    {
        _service = service;
    }

    [HttpPost]
    [Route("login")]
    public async Task<ActionResult<TokenResponse>> Login(
        [FromBody] LoginModel model
        )
    {
        return await Execute(() =>  _service.Login(model));
    }
    
    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register(
        [FromBody] RegisterModel model
    )
    {
        return await Execute(() => _service.CreateUser(model), Ok());
    }
}
using KebabMaster.Orders.DTOs;

namespace KebabMaster.Orders.Interfaces;

public interface IUserManagementService
{
    public Task CreateUser(RegisterModel model);
    public Task<TokenResponse> Login(LoginModel model);
}
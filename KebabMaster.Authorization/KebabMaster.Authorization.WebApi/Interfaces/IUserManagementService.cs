using KebabMaster.Orders.DTOs;

namespace KebabMaster.Authorization.Interfaces;

public interface IUserManagementService
{
    public Task CreateUser(RegisterModel model);
    public Task<TokenResponse> Login(LoginModel model);
    public Task<UserResponse> GetByFilter(UserRequest request);
    public Task DeleteUser(string email);

}
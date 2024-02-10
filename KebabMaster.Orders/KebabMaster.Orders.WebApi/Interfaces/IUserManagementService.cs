using KebabMaster.Orders.DTOs;

namespace KebabMaster.Orders.Interfaces;

public interface IUserManagementService
{
    public Task CreateUser(RegisterModel model);
    public Task<TokenResponse> Login(LoginModel model);
    public Task<IEnumerable<UserResponse>> GetByFilter(UserRequest request);
    public Task DeleteUser(Guid id);

    public Task UpdateUser(UserUpdateRequest request);
    public Task<UserResponse> GetById(Guid id);
}
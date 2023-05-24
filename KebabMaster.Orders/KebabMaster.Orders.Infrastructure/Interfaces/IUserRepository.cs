using KebabMaster.Orders.Domain.DTOs.Authorization;
using KebabMaster.Orders.Infrastructure.DTOs.Authorization;

namespace KebabMaster.Orders.Infrastructure.Interfaces;

public interface IUserRepository
{
    Task CreateUser(User user);
    Task<User?> GetUserByEmail(string email);
    Task<User?> GetUserByName(string name);
    
    Task<Role?> GetRoleByName(string name);
}
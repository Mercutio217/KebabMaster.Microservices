using KebabMaster.Authorization.Domain.Entities;

namespace KebabMaster.Authorization.Domain.Interfaces;

public interface IUserRepository
{
    Task CreateUser(User user);
    Task<User?> GetUserByEmail(string email);
    Task<User?> GetUserByName(string name);
    
    Task<Role?> GetRoleByName(string name);
}
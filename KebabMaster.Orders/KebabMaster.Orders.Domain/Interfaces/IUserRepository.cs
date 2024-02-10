using KebabMaster.Orders.Domain.DTOs;
using KebabMaster.Orders.Domain.Entities;
using KebabMaster.Orders.Domain.Filters;

namespace KebabMaster.Orders.Domain.Interfaces;

public interface IUserRepository
{
    Task CreateUser(User user);
    Task<IEnumerable<User>> GetUserByFilter(UserFilter filter);
    Task<User> GetUserByEmail(string email);
    Task DeleteUser(Guid id);
    Task<Role?> GetRoleByName(string name);
    Task UpdateUser(UserUpdateModel model);
    Task<User> GetById(Guid id);
}
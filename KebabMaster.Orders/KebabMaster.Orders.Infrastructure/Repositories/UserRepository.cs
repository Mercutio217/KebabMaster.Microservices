using KebabMaster.Orders.Domain.DTOs.Authorization;
using KebabMaster.Orders.Infrastructure.Database;
using KebabMaster.Orders.Infrastructure.DTOs.Authorization;
using KebabMaster.Orders.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace KebabMaster.Orders.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;

    public UserRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public Task CreateUser(User user)
    {
        _context.Users.AddAsync(user);
        return _context.SaveChangesAsync();
    }

    public Task<User?> GetUserByEmail(string email)
    {
        return _context.Users
            .Include(u => u.Roles)
            .FirstOrDefaultAsync(user => user.Email == email);
    }

    public Task<User?> GetUserByName(string name)
    {
        return _context.Users
            .Include(u => u.Roles)
            .FirstOrDefaultAsync(user => user.Name == name);
    }

    public Task<Role?> GetRoleByName(string name)
    {
        return _context.Roles.FirstOrDefaultAsync(role => role.Name == name);
    }
}
using KebabMaster.Authorization.Domain.Entities;
using KebabMaster.Authorization.Domain.Interfaces;
using KebabMaster.Authorization.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace KebabMaster.Authorization.Infrastructure.Repositories;

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
        _context.Database.EnsureCreated();
        return _context.Roles.FirstOrDefaultAsync(role => role.Name == name);
    }
}
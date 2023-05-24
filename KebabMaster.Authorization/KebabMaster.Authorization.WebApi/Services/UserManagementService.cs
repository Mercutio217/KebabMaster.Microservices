﻿using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using KebabMaster.Authorization.Domain.Entities;
using KebabMaster.Authorization.Domain.Exceptions;
using KebabMaster.Authorization.Domain.Interfaces;
using KebabMaster.Authorization.Interfaces;
using KebabMaster.Orders.DTOs;
using Microsoft.IdentityModel.Tokens;

namespace KebabMaster.Authorization.Services;

public class UserManagementService : IUserManagementService
{
    private readonly IConfiguration _configuration;
    private readonly IUserRepository _repository;
    private readonly IApplicationLogger _logger;

    public UserManagementService(
        IConfiguration configuration,
        IUserRepository repository,
        IApplicationLogger logger)
    {
        _configuration = configuration;
        _repository = repository;
        _logger = logger;
    }

    public async Task CreateUser(RegisterModel model)
    {
        User user = User.Create(model.Email, model.UserName, model.Name, model.Surname);

        var hash = HashString(model.Password);

        user.PaswordHash = hash;

        var role = await _repository.GetRoleByName("Admin");
        user.Roles = new List<Role>() { role };

        await _repository.CreateUser(user);
    }


    public async Task<TokenResponse> Login(LoginModel model)
    {
        var user = await _repository.GetUserByEmail(model.Email);
        if (user is null)
            throw new UnauthorizedException(model.Email);

        string hashString = HashString(model.Password);
        
        if (hashString != user.PaswordHash)
            throw new Exception();

        var claims = new List<Claim>()
        {
            new (ClaimTypes.Name, user.UserName),
            new (JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };

        foreach (var role in user.Roles)
        {
            claims.Add(new Claim(ClaimTypes.Role,role.Name));
        }
        
        var token = GetToken(claims);

        return new TokenResponse()
        {
            ExpiresAt = token.ValidTo,
            Token = new JwtSecurityTokenHandler().WriteToken(token),
        };
    }
    
    private JwtSecurityToken GetToken(List<Claim> authClaims)
    {
        SymmetricSecurityKey authSigningKey = 
            new (Encoding.UTF8.GetBytes(_configuration["TokenData:Secret"]));

        var token = new JwtSecurityToken(
            issuer: _configuration["TokenData:ValidIssuer"],
            audience: _configuration["TokenData:ValidAudience"],
            expires: DateTime.Now.AddHours(3),
            claims: authClaims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
        );

        return token;
    }
    private  string HashString(string text)
    {
        if (String.IsNullOrEmpty(text))
            throw new MissingMandatoryPropertyException<User>("Password");

        using var sha = new System.Security.Cryptography.SHA256Managed();
        byte[] textBytes = Encoding.UTF8.GetBytes(text);
        byte[] hashBytes = sha.ComputeHash(textBytes);
        
        // Convert back to a string, removing the '-' that BitConverter adds
        string hash = BitConverter
            .ToString(hashBytes)
            .Replace("-", String.Empty);

        return hash;
    }
    
    private async Task Execute(Func<Task> function)
    {
        try
        {
            await function();
        }
        catch (ApplicationValidationException validationException)
        {
            _logger.LogValidationException(validationException);
        }
        catch (Exception exception)
        {
            _logger.LogException(exception);
        }
    }

    private async Task<T> Execute<T>(Func<Task<T>> function)
    {
        try
        {
            return await function();
        }
        catch (ApplicationValidationException validationException)
        {
            _logger.LogValidationException(validationException);
            throw;
        }
        catch (Exception exception)
        {
            _logger.LogException(exception);
            throw;
        }
    }
}
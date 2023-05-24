using System.Net.Mail;
using KebabMaster.Orders.Domain.DTOs.Authorization;
using KebabMaster.Orders.Domain.Exceptions;

namespace KebabMaster.Orders.Infrastructure.DTOs.Authorization;

public class User
{
    public string Email { get; set; }
    public string UserName { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string PaswordHash { get; set; }
    public IEnumerable<Role> Roles { get; set; }
    
    public User() { }

    public User(string email, string userName, string name, string surname)
    {
        Email = email;
        UserName = userName;
        Name = name;
        Surname = surname;
    }

    public static User Create(string email, string username, string name, string surname)
    {
        try
        {
            _ = new MailAddress(email);
        }
        catch
        {
            throw new InvalidEmailFormatException(email);
        }

        if (username is null)
            throw new MissingMandatoryPropertyException<User>(nameof(Name));

        if (name is null)
            throw new MissingMandatoryPropertyException<User>(nameof(Name));
        
        if (surname is null)
            throw new MissingMandatoryPropertyException<User>(nameof(Name));

        return new User(email, username, name, surname);
    }
    
}

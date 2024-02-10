namespace KebabMaster.Orders.Domain.DTOs;

public class UserUpdateModel
{
    public Guid Id { get;  set; }
    public string? UserName { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
}
namespace KebabMaster.Orders.DTOs;

public class UserUpdateRequest : UserRequest
{
    public Guid Id { get; private set; }
}
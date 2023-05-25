namespace KebabMaster.Orders.Domain.Interfaces;

public interface IMenuRepository
{
    public Task<MenuItem> GetMenuItemById(Guid id);
}
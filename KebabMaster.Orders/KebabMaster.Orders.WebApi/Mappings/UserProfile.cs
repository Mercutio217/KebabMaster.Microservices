using AutoMapper;
using KebabMaster.Orders.Domain.DTOs;
using KebabMaster.Orders.Domain.Entities;
using KebabMaster.Orders.Domain.Filters;
using KebabMaster.Orders.DTOs;

namespace KebabMaster.Orders.Mappings;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<UserRequest, UserFilter>();
        CreateMap<User, UserResponse>();
        CreateMap<UserUpdateRequest, UserUpdateModel>()
            .ConstructUsing(req => new ()
            {
                Id = req.Id,
                Name = req.Name,
                Surname = req.Surname,
                UserName = req.UserName
            });
    }
    
    
}
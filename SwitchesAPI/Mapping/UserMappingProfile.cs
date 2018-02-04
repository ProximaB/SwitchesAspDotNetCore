using AutoMapper;
using SwitchesAPI.DB.DbModels;
using SwitchesAPI.Models;

namespace SwitchesAPI.Mapping
{
    public class UserMappingProfile : Profile
    {
        UserMappingProfile()
        {
            CreateMap<UserRequest, User>();
            CreateMap<User, UserResponse>();
        }
    }
}

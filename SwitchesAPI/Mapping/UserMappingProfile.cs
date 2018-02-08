using AutoMapper;
using SwitchesAPI.DB.DbModels;
using SwitchesAPI.Models;

namespace SwitchesAPI.Mapping
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<UserRequest, User>();
            CreateMap<User, UserResponse>();
            CreateMap<UserRequestPut, User>();
        }
    }
}

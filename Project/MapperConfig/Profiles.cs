using Project.Models.DTO;
using Project.MapperConfig;
using AutoMapper;
using Project.Models;
namespace Project.MapperConfig
{
    public class MapperProfiles : Profile
    {
        public  MapperProfiles()
        {
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<User, UserAllDTO>().ReverseMap();
            CreateMap<User,UserPutDTO>().ReverseMap();
            CreateMap<User,UserPostDTO>().ReverseMap();
        }
    }
}

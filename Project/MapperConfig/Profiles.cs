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
            CreateMap<User, UserDTO>().ForMember(
                usrRole => usrRole.RoleName
            , roleDTO => roleDTO
            .MapFrom(src => src.Role.RoleName)).ReverseMap();

            CreateMap<User, UserAllDTO>().ForMember(
                usrRole => usrRole.RoleName
            , roleDTO => roleDTO
            .MapFrom(src => src.Role.RoleName)).ReverseMap();

            CreateMap<User,UserPutDTO>().ReverseMap();

            CreateMap<User, UserPostDTO>()
                .ForMember(roleId => roleId.RoleId,
                roleIdDTO => roleIdDTO
                .MapFrom(src => src.Role.RoleId)).ReverseMap();

            CreateMap<User, PutUserRoleDTO>()
                .ForMember(roleId => roleId.RoleId, 
                roleIdDTO => roleIdDTO
                .MapFrom(src => src.Role.RoleId)).ReverseMap();
            CreateMap<Role, RolesDTO>().ReverseMap();
            CreateMap<Role, PostRoleDTO>().ReverseMap();
        }
    }
}

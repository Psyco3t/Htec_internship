using AutoMapper;
using Project.Models;
namespace Project.MapperConfig
{
 public class MapperConfig
 {
  public static Mapper InitializeAutoMapper()
  {
   var config = new MapperConfiguration(cfg =>
   {
    cfg.CreateMap<User, UserDTO>();
   });
   var mapper = new Mapper(config);
   return mapper;
  }
 }
}

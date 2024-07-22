using AutoMapper;
using Project.Models;
using Project.Models.DTO;
namespace Project.MapperConfig
{
    public class MapperConfig
    {
        public static Mapper InitializeAutoMapper()
        {
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MapperProfiles());
            });
            IMapper mapper = mapperConfig.CreateMapper();
            return (Mapper)mapper;
        }
    }
}

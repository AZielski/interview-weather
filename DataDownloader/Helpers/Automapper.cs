using AutoMapper;
using DataTemplates;

namespace DataDownloader.Helpers;

public class Mapper
{
    private static readonly MapperConfiguration Config = new(cfg =>
    {
        cfg.CreateMap<Hourly, RedisTemplate>();
    });
    
    public static readonly IMapper MapperInstance = Config.CreateMapper();
}
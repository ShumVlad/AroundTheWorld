using AroundTheWorld_Backend.DTOs;
using AroundTheWorld_Persistence.Models;
using AutoMapper;

namespace AroundTheWorld_Backend
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<LocationDTO, Location>();
            CreateMap<RouteDTO, Route>();
            CreateMap<LocationRouteDTO, LocationRoute>();
        }
    }
}

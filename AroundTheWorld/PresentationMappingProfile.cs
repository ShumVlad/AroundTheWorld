using AroundTheWorld.ViewModels;
using AroundTheWorld_Backend.DTOs;
using AutoMapper;

namespace AroundTheWorld
{
    public class PresentationMappingProfile : Profile
    {
        public PresentationMappingProfile()
        {
            CreateMap<LocationViewModel, LocationDTO>();
            CreateMap<RouteViewModel, RouteDTO>();
        }
    }
}

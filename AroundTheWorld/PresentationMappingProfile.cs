using AroundTheWorld.ViewModels;
using AroundTheWorld.ViewModels.IdentityModels;
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
            CreateMap<LocationRouteViewModel, LocationRouteDTO>();
            CreateMap<GetRouteDto, GetRouteViewModel>();
            CreateMap<UpdateSensorLocationViewModel, UpdateSensorLocationDto>();
        }
    }
}

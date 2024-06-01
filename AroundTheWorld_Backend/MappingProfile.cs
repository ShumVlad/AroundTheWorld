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
            CreateMap<CreateRouteDTO, Route>();
            CreateMap<LocationRouteDTO, LocationRoute>();
            CreateMap<GetRoute, GetRouteDto>();
            CreateMap<UserInGroup, UserInGroupDto>();
            CreateMap<UserGroupDto, UserGroup>();
            CreateMap<GetUserPosition, GetUserPositionDto>();
        }
    }
}

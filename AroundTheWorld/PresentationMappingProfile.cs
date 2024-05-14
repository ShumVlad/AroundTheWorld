using AroundTheWorld.ViewModels;
using AroundTheWorld_Backend.DTOs;
using AroundTheWorld_Persistence.Models;
using AutoMapper;

namespace AroundTheWorld
{
    public class PresentationMappingProfile : Profile
    {
        public PresentationMappingProfile()
        {
            CreateMap<LocationViewModel, LocationDTO>();
        }
    }
}

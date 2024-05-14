using Microsoft.AspNetCore.Mvc;
using AroundTheWorld_Backend.Interfaces;
using AroundTheWorld.ViewModels;
using AutoMapper;
using AroundTheWorld_Backend.DTOs;

namespace AroundTheWorld.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private ILocationService _locationService;
        private readonly IMapper _mapper;

        private LocationController(ILocationService locationService, IMapper mapper)
        {
            _locationService = locationService;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("Create")]
        internal async Task<bool> Create([FromBody] LocationViewModel model)
        {
            if (model == null)
            {
                return false;
            }
            var locationDto = _mapper.Map<LocationDTO>(model);
            bool result = await _locationService.Create(locationDto);
            return result;
        }
    }
}

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

        public LocationController(ILocationService locationService, IMapper mapper)
        {
            _locationService = locationService;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("Create")]
        public async Task<bool> Create([FromBody] LocationViewModel model)
        {
            if (model == null)
            {
                return false;
            }
            var locationDto = _mapper.Map<LocationDTO>(model);
            bool result = await _locationService.Create(locationDto);
            return result;
        }

        [HttpPost]
        [Route("Delete")]
        public async Task<bool> Delete([FromBody] string id)
        {
            if (id == null)
            {
                return false;
            }
            bool result = await _locationService.Delete(id);
            return result;
        }
    }
}

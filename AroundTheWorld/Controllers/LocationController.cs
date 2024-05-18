using Microsoft.AspNetCore.Mvc;
using AroundTheWorld_Backend.Interfaces;
using AroundTheWorld.ViewModels;
using AutoMapper;
using AroundTheWorld_Backend.DTOs;
using AroundTheWorld_Persistence.Models;

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
                throw new ArgumentNullException(nameof(model));
            }
            var locationDto = _mapper.Map<LocationDTO>(model);
            bool result = await _locationService.Create(locationDto);
            return result;
        }

        [HttpDelete]
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

        [HttpPut]
        [Route("Put")]
        public async Task<bool> Update([FromBody] Location location)
        {
            if (location == null)
            {
                return false;
            }
            bool result = await _locationService.Update(location);
            return result;
        }

        [HttpGet]
        [Route("Get")]
        public Location Get([FromBody] string id)
        {
            if (id == null)
            {
                //return ;
            }
            Location result = _locationService.Get(id);
            return result;
        }
    }
}

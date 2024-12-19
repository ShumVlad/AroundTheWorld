using System.Collections.Generic;
using System.Threading.Tasks;
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
        private readonly ILocationService _locationService;
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
        public async Task<bool> DeleteLocation([FromBody] string id)
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
        public async Task<bool> UpdateLocation([FromBody] Location location)
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
        public async Task<Location> GetLocation(string id)
        {
            if (id == null)
            {
                throw new Exception(nameof(id));
            }
            Location result = await _locationService.Get(id);
            return result;
        }

        [HttpGet]
        [Route("GetPaginated")]
        public async Task<ActionResult<List<Location>>> GetPaginated(int page = 1, int pageSize = 2)
        {
            var locations = await _locationService.GetPaginatedLocations(page, pageSize);
            return Ok(locations);
        }

        [HttpGet]
        [Route("GetAllnotHotelLocations")]
        public async Task<List<Location>> GetAllnotHotelLocations()
        {
            var locations = await _locationService.GetAllnotHotelLocations();
            return locations;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<List<Location>> GetAll()
        {
            var locations = await _locationService.GetAll();
            return locations;
        }
    }
}

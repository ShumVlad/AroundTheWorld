using AroundTheWorld.ViewModels;
using AroundTheWorld_Backend.DTOs;
using AroundTheWorld_Backend.Interfaces;
using AroundTheWorld_Backend.Services;
using AroundTheWorld_Persistence.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AroundTheWorld.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationRouteController : ControllerBase
    {
        private readonly ILocationRouteService _locationRouteService;
        private readonly IMapper _mapper;

        public LocationRouteController(ILocationRouteService locationRouteService, IMapper mapper)
        {
            _locationRouteService = locationRouteService;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("Create")]
        public async Task<bool> Create(LocationRouteViewModel model)
        {
            if(model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            LocationRouteDTO locationRouteDTO = _mapper.Map<LocationRouteDTO>(model);
            var result = await _locationRouteService.AddLocationRoute(locationRouteDTO);
            return result;
        }

        [HttpDelete]
        [Route("Delete")]
        public async Task<bool> Delete(string id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }
            var result = await _locationRouteService.DeleteLocationRoute(id);
            return result;
        }
    }
}

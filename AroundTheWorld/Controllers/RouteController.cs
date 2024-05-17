using AroundTheWorld.ViewModels;
using AroundTheWorld_Backend.DTOs;
using AroundTheWorld_Backend.Interfaces;
using AroundTheWorld_Persistence.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace AroundTheWorld.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RouteController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IRouteService _routeService;

        public RouteController(IMapper mapper, IRouteService routeService)
        {
            _mapper = mapper;
            _routeService = routeService;
        }

        [HttpPost]
        [Route("Create")]
        public async Task<bool> Create(RouteViewModel model)
        {
            if(model == null)
            {
                throw new ArgumentNullException("model");
            }
            var routeDTO = _mapper.Map<RouteDTO>(model);
            bool result = await _routeService.Create(routeDTO);
            return result;
        }

        [HttpPost]
        [Route("Delete")]
        public async Task<bool> Delete(string id)
        {
            if (id == null)
            {
                throw new ArgumentNullException("model");
            }
            bool result = await _routeService.Delete(id);
            return result;
        }

        [HttpPost]
        [Route("Get")]
        public AroundTheWorld_Persistence.Models.Route Get(string id)
        {
            if (id == null)
            {
                throw new ArgumentNullException();
            }
            AroundTheWorld_Persistence.Models.Route result = _routeService.Get(id);
            return result;
        }

        [HttpPost]
        [Route("Update")]
        public async Task<bool> Get(AroundTheWorld_Persistence.Models.Route route)
        {
            if (route == null)
            {
                throw new ArgumentNullException();
            }
            var result = await _routeService.Update(route);
            return result;
        }
    }
}
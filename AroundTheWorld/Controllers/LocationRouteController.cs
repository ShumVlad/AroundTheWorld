using AroundTheWorld_Backend.Services;
using AroundTheWorld_Persistence.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AroundTheWorld.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationRouteController : Controller
    {
        private readonly LocationRouteService _locationRouteService;

        LocationRouteController(LocationRouteService locationRouteService)
        {
            _locationRouteService = locationRouteService;
        }
        [HttpPost]
        [Route("Create")]
        public async Task<bool> Create(LocationRoute locationRoute)
        {
            if(locationRoute == null)
            {
                throw new ArgumentNullException(nameof(locationRoute));
            }
            var result = await _locationRouteService.CreateLocationRoute(locationRoute);
            return result;
        }
    }
}

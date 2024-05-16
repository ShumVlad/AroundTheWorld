using AroundTheWorld_Backend.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AroundTheWorld.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RouteController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IRouteService _routeService;
    }
}

using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AroundTheWorld.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationRouteController : Controller
    {
        private readonly IMapper _mapper;
    }
}

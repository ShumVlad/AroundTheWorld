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
    public class UserPositionController : ControllerBase
    {
        private readonly IUserPositionService _userPositionService;

        public UserPositionController(IUserPositionService userGroupService, IMapper mapper)
        {
            _userPositionService = userGroupService;
        }

        [HttpPut]
        [Route("Update")]
        public async Task<bool> Update(UserPositionDto userPosition)
        {
            if (userPosition == null)
            {
                throw new ArgumentNullException();
            }
            var result = await _routeService.Update(userPosition);
            return result;
        }
    }
}

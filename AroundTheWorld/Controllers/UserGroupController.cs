using AroundTheWorld_Backend;
using AroundTheWorld_Backend.DTOs;
using AroundTheWorld_Backend.Interfaces;
using AroundTheWorld_Persistence.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace AroundTheWorld.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserGroupController : ControllerBase
    {
        private readonly IUserGroupService _userGroupService;
        private readonly IMapper _mapper;

        public UserGroupController(IUserGroupService userGroupService, IMapper mapper)
        {
            _userGroupService = userGroupService;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> Add([FromBody] UserGroupDto userGroup)
        {
            if (userGroup == null)
            {
                return BadRequest("UserGroup cannot be null");
            }

            var result = await _userGroupService.AddUserToGroup(userGroup);

            if (result)
            {
                return Ok(result);
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error adding user to group");
            }
        }

        [HttpDelete]
        [Route("Delete")]
        public async Task<bool> Remove(string id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }
            var result = await _userGroupService.RemoveUserFromGroup(id);
            return result;
        }

        [HttpGet]
        [Route("GetUsersFromGroup")]
        public async Task<List<UserInGroupDto>> GetUsersFromGroup(string groupId)
        {
            if (groupId == null)
            {
                throw new ArgumentNullException(nameof(groupId));
            }
            List <UserInGroupDto> result = await _userGroupService.GetUsers(groupId);
            return result;
        }

        [HttpGet]
        [Route("GetUserLocations")]
        public async Task<List<GetUserPositionDto>> GetUserLocations(string groupId)
        {
            if (groupId == null)
            {
                throw new ArgumentNullException(nameof(groupId));
            }
            List<GetUserPositionDto> result = await _userGroupService.GetUserLocations(groupId);
            return result;
        }
    }
}

using AroundTheWorld_Backend;
using AroundTheWorld_Backend.Interfaces;
using AroundTheWorld_Persistence.Models;
using Microsoft.AspNetCore.Mvc;

namespace AroundTheWorld.Controllers
{
    public class UserGroupController : ControllerBase
    {
        private readonly IUserGroupService _userGroupService;

        public UserGroupController(IUserGroupService userGroupService)
        {
            _userGroupService = userGroupService;
        }

        [HttpPost]
        [Route("Add")]
        public async Task<bool> Add(UserGroup userGroup)
        {
            if (userGroup == null)
            {
                throw new ArgumentNullException(nameof(userGroup));
            }
            var result = await _userGroupService.AddUserToGroup(userGroup);
            return result;
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
    }
}

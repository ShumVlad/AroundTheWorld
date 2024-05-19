using AroundTheWorld_Backend.Interfaces;
using AroundTheWorld_Persistence.Models;
using Microsoft.AspNetCore.Mvc;

namespace AroundTheWorld.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupController : ControllerBase
    {
        private IGroupService _groupService;

        public GroupController(IGroupService groupService)
        {
            _groupService = groupService;
        }

        [HttpGet]
        [Route("Get")]
        public Group Get(string id)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentNullException(nameof(id)); ;
            var result = _groupService.Get(id);
            return result;
        }

        [HttpPost]
        [Route("Add")]
        public async Task<bool> Add(Group group)
        {
            if(group == null) throw new ArgumentNullException(nameof(group));
            var result = await _groupService.Add(group);
            return result;
        }

        [HttpPut]
        [Route("Update")]
        public async Task<bool> Update(Group group)
        {
            if(group == null) throw new ArgumentNullException(nameof(group));
            var result = await (_groupService.Update(group));
            return result;
        }
        [HttpDelete]
        [Route("Delete")]
        public async Task<bool> Delete(string id)
        {
            if(id == null) throw new ArgumentNullException(nameof(id));
            var result = await _groupService.Delete(id);
            return result;
        }
    }
}

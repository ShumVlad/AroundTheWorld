using AroundTheWorld_Backend.Interfaces;
using AroundTheWorld_Persistence.Models;

namespace AroundTheWorld_Backend.Services
{
    public class GroupService : IGroupService
    {
        private UnitOfWork _unitOfWork;

        public GroupService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Add(Group group)
        {
            group.Id = Guid.NewGuid().ToString();
            await _unitOfWork.GroupRepository.Add(group);
            _unitOfWork.Save();
            return true;
        }

        public async Task<bool> Delete(string id)
        {
            await _unitOfWork.GroupRepository.Delete(id);
            _unitOfWork.Save();
            return true;
        }

        public async Task<bool> Update(Group group)
        {
            await _unitOfWork.GroupRepository.Update(group);
            _unitOfWork.Save();
            return true;
        }

        public async Task<Group> Get(string id) 
        {
            Group result = await _unitOfWork.GroupRepository.Get(id);
            return result;
        }

        public async Task<Group> GetByRouteId(string routeId)
        {
            Group result = await _unitOfWork.GroupRepository.GetGroupByRouteId(routeId);
            return result;
        }
    }
}

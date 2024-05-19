

using AroundTheWorld_Persistence.Models;

namespace AroundTheWorld_Backend.Services
{
    public class GroupService
    {
        private UnitOfWork _unitOfWork;

        public GroupService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Add(Group group)
        {
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

        public Group Get(string id) 
        {
            Group result = _unitOfWork.GroupRepository.Get(id);
            return result;
        }
    }
}

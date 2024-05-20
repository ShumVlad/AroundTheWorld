using AroundTheWorld_Backend.DTOs;
using AroundTheWorld_Backend.Interfaces;
using AroundTheWorld_Persistence.Models;
using AutoMapper;

namespace AroundTheWorld_Backend.Services
{
    public class UserGroupService : IUserGroupService
    {
        private UnitOfWork _unitOfWork;

        public UserGroupService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> AddUserToGroup(UserGroup userGroup)
        {
            if (userGroup == null)
            {
                throw new ArgumentNullException(nameof(userGroup));
            }
            var user = _unitOfWork.UserRepository.Get(userGroup.UserId);
            if (user == null)
            {
                throw new ArgumentException("Invalid LocationId");
            }

            var groupExists = _unitOfWork.GroupRepository.Get(userGroup.GroupId) != null;
            if (!groupExists)
            {
                throw new ArgumentException("Invalid RouteId");
            }
            userGroup.Id = Guid.NewGuid().ToString();
            await _unitOfWork.UserGroupRepository.Add(userGroup);
            _unitOfWork.Save();
            return true;
        }

        public async Task<bool> RemoveUserFromGroup(string id)
        {
            await _unitOfWork.UserGroupRepository.Delete(id);
            _unitOfWork.Save();
            return true;
        }

        public List<UserGroup> GetUsers(string groupId)
        {
            
        }
    }
}

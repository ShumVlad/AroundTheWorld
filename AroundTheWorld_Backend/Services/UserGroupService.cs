using AroundTheWorld_Backend.DTOs;
using AroundTheWorld_Backend.Interfaces;
using AroundTheWorld_Persistence;
using AroundTheWorld_Persistence.Models;
using AroundTheWorld_Persistence.Repositories.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;

namespace AroundTheWorld_Backend.Services
{
    public class UserGroupService : IUserGroupService
    {
        private UnitOfWork _unitOfWork;
        private IUserGroupExtraRepository _extraRepository;
        private IMapper _mapper;

        public UserGroupService(UnitOfWork unitOfWork, IUserGroupExtraRepository extraRepository, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _extraRepository = extraRepository;
            _mapper = mapper;
        }

        public async Task<bool> AddUserToGroup(UserGroupDto userGroup)
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
            UserGroup result = _mapper.Map<UserGroup>(userGroup);
            await _unitOfWork.UserGroupRepository.Add(result);
            _unitOfWork.Save();
            return true;
        }

        public async Task<bool> RemoveUserFromGroup(string id)
        {
            await _unitOfWork.UserGroupRepository.Delete(id);
            _unitOfWork.Save();
            return true;
        }

        public async Task<List<UserInGroupDto>> GetUsers(string groupId)
        {
            List<UserInGroup> usersInGroup = await _extraRepository.GetUserIdsFromGroup(groupId);
            List<UserInGroupDto> result = _mapper.Map<List<UserInGroupDto>>(usersInGroup);
            return result;
        }
    }
}

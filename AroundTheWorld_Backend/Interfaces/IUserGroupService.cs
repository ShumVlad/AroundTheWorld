using AroundTheWorld_Backend.DTOs;
using AroundTheWorld_Persistence.Models;

namespace AroundTheWorld_Backend.Interfaces
{
    public interface IUserGroupService
    {
        Task<bool> AddUserToGroup(UserGroup userGroup);
        Task<bool> RemoveUserFromGroup(string id);
    }
}

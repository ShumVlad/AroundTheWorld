using AroundTheWorld_Persistence.Models;
using AroundTheWorld_Persistence.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace AroundTheWorld_Persistence.Repositories
{
    public class UserGroupExtraRepository : IUserGroupExtraRepository
    {
        private readonly AroundTheWorldDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserGroupExtraRepository(AroundTheWorldDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<List<UserInGroup>> GetUserIdsFromGroup(string groupId)
        {
            // First, fetch user IDs from the userGroups table
            var userIds = await _context.userGroups
                .Where(ug => ug.GroupId == groupId)
                .Select(ug => ug.UserId)
                .ToListAsync();

            // Next, fetch user details and roles using _userManager
            var users = new List<UserInGroup>();
            foreach (var userId in userIds)
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user != null)
                {
                    var role = (await _userManager.GetRolesAsync(user)).FirstOrDefault();
                    users.Add(new UserInGroup
                    {
                        UserName = user.UserName,
                        UserRole = role,
                        Email = user.Email,
                        Id = user.Id
                    });
                }
            }

            return users;
        }
    }
}

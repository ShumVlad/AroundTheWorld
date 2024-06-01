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

        public async Task<List<UserInGroup>> GetUserdFromGroup(string groupId)
        {
            List<UserGroup> userGroups = await _context.userGroups
                .Where(ug => ug.GroupId == groupId)
                .Select(ug => ug)
                .ToListAsync();

            var users = new List<UserInGroup>();
            foreach (var userGroup in userGroups)
            {
                var user = await _userManager.FindByIdAsync(userGroup.UserId);
                if (user != null)
                {
                    var role = (await _userManager.GetRolesAsync(user)).FirstOrDefault();
                    users.Add(new UserInGroup
                    {
                        UserName = user.UserName,
                        UserRole = role,
                        Email = user.Email,
                        UserId = user.Id,
                        Id = userGroup.Id
                    });
                }
            }

            return users;
        }
    }
}

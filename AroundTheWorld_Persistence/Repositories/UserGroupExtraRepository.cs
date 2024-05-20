using AroundTheWorld_Persistence.Models;
using AroundTheWorld_Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace AroundTheWorld_Persistence.Repositories
{
    public class UserGroupExtraRepository : IUserGroupExtraRepository
    {
        private readonly AroundTheWorldDbContext _context;

        public UserGroupExtraRepository(AroundTheWorldDbContext context)
        {
            _context = context;
        }

        public List<string> GetUserIdsFromGroup(string groupId)
        {
            List<UserGroup> userGroups = new List<UserGroup>();

            var result = from uG in _context.userGroups
                         where uG.GroupId.Equals(groupId)
                         select uG;
            userGroups = result.ToList();

            List<string> userIds = new List<string>();
            for (int i = 0; i < userGroups.Count; i++)
            {
                userIds.Add(userGroups[i].UserId);
            }
            return userIds;
        }
    }
}

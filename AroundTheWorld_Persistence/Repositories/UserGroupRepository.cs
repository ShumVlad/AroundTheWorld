using AroundTheWorld_Persistence.Models;
using AroundTheWorld_Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AroundTheWorld_Persistence.Repositories
{
    public class UserGroupRepository : IUserGroupExtraRepository
    {
        private readonly AroundTheWorldDbContext _context;
        private readonly DbSet<UserGroup> _dbSet;

        public UserGroupRepository(AroundTheWorldDbContext context, DbSet<UserGroup> dbSet)
        {
            _context = context;
            _dbSet = dbSet;
        }

        public List<string> GetUserIdsFromGroup(string groupId) 
        {
            List<UserGroup> userGroups = new List<UserGroup>();

            var result = from uG in _dbSet
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

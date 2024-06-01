using System.ComponentModel.Design;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AroundTheWorld_Persistence.Models;
using AroundTheWorld_Persistence.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AroundTheWorld_Persistence.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly AroundTheWorldDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly DbSet<T> _dbSet;

        public Repository(AroundTheWorldDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _dbSet = _context.Set<T>();
            _userManager = userManager;
        }

        public async Task Add(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task Delete(string id)
        {
            T existing = await Get(id);
            _dbSet.Remove(existing);
        }

        public async Task<T> Get(string id)
        {
            T result = await _dbSet.FindAsync(id);
            return result;
        }

        public async Task<List<T>> GetAll()
        {
            return _dbSet.ToList();
        }

        public Task Update(T entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            return Task.CompletedTask;
        }

        public async Task<List<T>> GetPaginated(int page, int pageSize)
        {
            return await _dbSet.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        public async Task<List<GetRoute>> GetUserRoutes(string userId)
        {
            List<GetRoute> routes = await (from ug in _context.userGroups
                                                       join g in _context.Groups on ug.GroupId equals g.Id
                                                       join r in _context.Routes on g.RouteId equals r.Id
                                                       join c in _context.Companies on r.CompanyId equals c.Id
                                                       where ug.UserId == userId
                                                       select new GetRoute
                                                       {
                                                           Id = r.Id,
                                                           Name = r.Name,
                                                           Description = r.Description,
                                                           IsFinished = r.IsFinished,
                                                           StartDateTime = r.StartDateTime,
                                                           CompanyName = c.Name,
                                                           CompanyId = c.Id
                                                       }).ToListAsync();
            return routes;
        }

        public async Task<List<GetRoute>> GetCompanyRoutes(string companyId)
        {
            List<GetRoute> routes = await (from r in _context.Routes
                                           join c in _context.Companies on r.CompanyId equals c.Id
                                           where r.CompanyId == companyId
                                           select new GetRoute
                                           {
                                               Id = r.Id,
                                               Name = r.Name,
                                               Description = r.Description,
                                               IsFinished = r.IsFinished,
                                               CompanyName = c.Name,
                                               CompanyId = c.Id
                                           }).ToListAsync();
            return routes;
        }

        public async Task<List<RentItem>> GetRentItemsForCompany(string companyId)
        {
            List<RentItem> rentItems = await (from rentItem in _context.RentItems
                                              where rentItem.CompanyId.Equals(companyId)
                                              select rentItem).ToListAsync();
            return rentItems;
        }

        public async Task<List<Route>> GetRoutesWithLocation(string locationId)
        {
            List<Route> routes = await (from r in _context.Routes
                                           join lr in _context.LocationRoutes on r.Id equals lr.RouteId
                                           where lr.LocationId == locationId
                                           select r).ToListAsync();
            return routes;
        }

        public async Task<AroundTheWorld_Persistence.Models.Group> GetGroupByRouteId(string routeId)
        {
            AroundTheWorld_Persistence.Models.Group group = await _context.Groups
                               .FirstOrDefaultAsync(g => g.RouteId == routeId);
            return group;
        }
        public async Task<List<GetUserPosition>> GetUserLocations(string groupId)
        {
            var userGroups = await _context.userGroups
                .Where(ug => ug.GroupId == groupId)
                .ToListAsync();

            var userIds = userGroups.Select(ug => ug.UserId).ToList();
            var userPositions = await _context.UserPositions
                .Where(up => userIds.Contains(up.UserId))
                .ToListAsync();

            var userPositionDtos = new List<GetUserPosition>();

            foreach (var userPosition in userPositions)
            {
                var user = await _userManager.FindByIdAsync(userPosition.UserId);
                if (user != null)
                {
                    userPositionDtos.Add(new GetUserPosition
                    {
                        Id = userPosition.Id,
                        Latitude = userPosition.Latitude,
                        Longitude = userPosition.Longitude,
                        UserId = userPosition.UserId,
                        UserName = user.UserName
                    });
                }
            }
            return userPositionDtos;
        }


    }
}

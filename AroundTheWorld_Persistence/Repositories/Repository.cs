using System.ComponentModel.Design;
using System.Linq;
using System.Threading.Tasks;
using AroundTheWorld_Persistence.Models;
using AroundTheWorld_Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AroundTheWorld_Persistence.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly AroundTheWorldDbContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(AroundTheWorldDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
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

        public async Task<Group> GetGroupByRouteId(string routeId)
        {
            Group group = await _context.Groups
                               .FirstOrDefaultAsync(g => g.RouteId == routeId);
            return group;
        }
    }
}

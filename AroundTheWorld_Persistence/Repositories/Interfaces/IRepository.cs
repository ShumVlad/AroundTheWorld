using AroundTheWorld_Persistence.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AroundTheWorld_Persistence.Repositories.Interfaces
{
    public interface IRepository<T>
    {
        Task Delete(string id);

        Task Update(T entity);

        Task Add(T entity);

        Task<T> Get(string id);

        Task<List<T>> GetAll();
        Task<List<T>> GetPaginated(int page, int pageSize);
        Task<List<GetRoute>> GetUserRoutes(string userId);
        Task<List<RentItem>> GetRentItemsForCompany(string companyId);
        Task<List<GetRoute>> GetCompanyRoutes(string companyId);
        Task<List<Route>> GetRoutesWithLocation(string locationId);
        Task<Group> GetGroupByRouteId(string routeId);
        Task<List<GetUserPosition>> GetUserLocations(string groupId);
        Task<string> GetUserPositionId(string userId);
    }
}

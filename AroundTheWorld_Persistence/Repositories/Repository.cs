using System.Linq;
using System.Threading.Tasks;
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
            var existing = Get(id);
            _dbSet.Remove(existing);
        }

        public T Get(string id)
        {
            return _dbSet.Find(id);
        }

        public List<T> GetAll()
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
    }
}

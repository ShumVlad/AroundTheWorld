using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AroudTheWorld.Persistence.Repositories
{
    public interface IRepository<T>
    {
        Task Delete(string id);

        Task Update(T entity);

        Task Add(T entity);

        T Get(string id);

        List<T> GetAll();
    }
}

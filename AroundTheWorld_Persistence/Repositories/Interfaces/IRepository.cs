﻿using System;
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

        T Get(string id);

        List<T> GetAll();
        Task<List<T>> GetPaginated(int page, int pageSize);
    }
}
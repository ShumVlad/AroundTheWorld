using AroundTheWorld_Persistence.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AroundTheWorld_Backend.Interfaces
{
    public interface ICompanyService
    {
        Task<string> Add(Company company);
        Task<string> Get(string companyId);
    }
}

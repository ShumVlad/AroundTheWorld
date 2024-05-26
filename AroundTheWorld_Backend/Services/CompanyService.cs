using AroundTheWorld_Backend.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AroundTheWorld_Backend.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly UnitOfWork _unit;

        public CompanyService(UnitOfWork unitOfWork)
        {
            _unit = unitOfWork;
        }

        public async Task<string> Add(CompanyService company)
        {

        }
        
    }
}

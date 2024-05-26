using AroundTheWorld_Backend.Interfaces;
using AroundTheWorld_Persistence.Models;
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

        public async Task<string> Add(Company company)
        {
            company.Id = Guid.NewGuid().ToString();
            await _unit.CompanyRepository.Add(company);
            _unit.Save();
            return company.Id;
        }
        
    }
}

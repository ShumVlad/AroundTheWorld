using AroundTheWorld_Backend.Interfaces;
using AroundTheWorld_Persistence.Models;
using Microsoft.AspNetCore.Mvc;

namespace AroundTheWorld.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _companyService;

        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        [HttpPost]
        [Route("Add")]
        public async Task<string> Add(Company company)
        {
            if(company == null)
            {
                throw new ArgumentNullException();
            }
            var result = await _companyService.Add(company);
            return result;
        }
    }
}

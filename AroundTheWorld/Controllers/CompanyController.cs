using Microsoft.AspNetCore.Mvc;

namespace AroundTheWorld.Controllers
{
    public class CompanyController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

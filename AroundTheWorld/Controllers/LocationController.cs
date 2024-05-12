using Microsoft.AspNetCore.Mvc;

namespace AroundTheWorld.Controllers
{
    public class LocationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

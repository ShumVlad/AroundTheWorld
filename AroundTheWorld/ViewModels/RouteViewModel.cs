using AroundTheWorld_Persistence.Models;

namespace AroundTheWorld.ViewModels
{
    public class RouteViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string CompanyId { get; set; }
        public List<Location> Locations { get; set; }
    }
}

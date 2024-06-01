using AroundTheWorld_Persistence.Models;

namespace AroundTheWorld.ViewModels
{
    public class GetRouteViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsFinished { get; set; }
        public DateTime StartDateTime { get; set; }
        public string CompanyId { get; set; }
        public string CompanyName { get; set; }
    }
}

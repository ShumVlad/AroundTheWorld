﻿using AroundTheWorld_Persistence.Models;

namespace AroundTheWorld.ViewModels
{
    public class CreateRouteViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsFinisched { get; set; }
        public string CompanyId { get; set; }
        public string GroupName { get; set; }
        public DateTime StartDateTime { get; set; }
        public List<Location> Locations { get; set; }
    }
}

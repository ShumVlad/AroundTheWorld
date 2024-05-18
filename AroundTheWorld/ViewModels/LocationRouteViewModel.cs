﻿using System.ComponentModel.DataAnnotations;

namespace AroundTheWorld.ViewModels
{
    public class LocationRouteViewModel
    {
        public int Order { get; set; }
        [Required]
        public string LocationId { get; set; }
        [Required]
        public string RouteId { get; set; }
    }
}
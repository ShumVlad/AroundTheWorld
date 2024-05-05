﻿using System.ComponentModel.DataAnnotations;

namespace AroundTheWorld_Persistence.Models
{
    public class Location
    {
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string Type { get; set; }
        public string CityId { get; set; }
    }
}
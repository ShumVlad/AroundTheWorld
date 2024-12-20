﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AroundTheWorld_Persistence.Models
{
    public class Group
    {
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }
        [Required]
        public string RouteId { get; set; }

        [ForeignKey(nameof(RouteId))]
        public virtual Route Route { get; set; }
    }
}

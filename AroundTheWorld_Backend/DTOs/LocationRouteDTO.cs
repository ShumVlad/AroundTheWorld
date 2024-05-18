using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AroundTheWorld_Backend.DTOs
{
    public class LocationRouteDTO
    {
        public int Order { get; set; }
        [Required]
        public string LocationId { get; set; }
        [Required]
        public string RouteId { get; set; }
    }
}

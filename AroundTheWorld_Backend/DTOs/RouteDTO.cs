using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AroundTheWorld_Backend.DTOs
{
    public class RouteDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string CompanyId { get; set; }
    }
}

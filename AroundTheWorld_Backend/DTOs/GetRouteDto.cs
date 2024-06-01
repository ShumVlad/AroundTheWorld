using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AroundTheWorld_Backend.DTOs
{
    public class GetRouteDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsFinished { get; set; }
        public DateTime StartDateTime { get; set; }
        public string CompanyId { get; set; }
        public string CompanyName { get; set;}
    }
}

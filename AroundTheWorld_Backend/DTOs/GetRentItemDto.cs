using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AroundTheWorld_Backend.DTOs
{
    public class GetRentItemDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string ImageLink { get; set; }
        public bool IsRented { get; set; }
        public string CompanyId { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}

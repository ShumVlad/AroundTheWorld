﻿using AroundTheWorld_Persistence.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AroundTheWorld_Backend.DTOs
{
    public class CreateRouteDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsFinisched { get; set; }
        public string CompanyId { get; set; }
        public string GroupName { get; set; }
        public DateTime StartDateTime { get; set; }
        public List<Location> Locations { get; set; }
    }
}

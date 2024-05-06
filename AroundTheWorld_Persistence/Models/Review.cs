using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AroundTheWorld_Persistence.Models
{
    public class Review
    {
        [Key]
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Location_Id { get; set; }
        public string User_Id { get; set; }

        [ForeignKey(nameof(Location_Id))]
        public virtual Location Location { get; set; }
        [ForeignKey(nameof(User_Id))]
        public virtual ApplicationUser User { get; set; }
    }
}

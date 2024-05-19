using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace AroundTheWorld_Persistence.Models
{
    public class Review
    {
        [Key]
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string LocationId { get; set; }
        public string UserId { get; set; }

        [ForeignKey(nameof(LocationId))]
        public virtual Location Location { get; set; }
        [ForeignKey(nameof(UserId))]
        public virtual ApplicationUser User { get; set; }
    }
}

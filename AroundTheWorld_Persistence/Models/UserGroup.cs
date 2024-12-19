using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AroundTheWorld_Persistence.Models
{
    public class UserGroup
    {
        [Key]
        public string Id { get; set; }
        public string UserId { get; set; }
        public string GroupId { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual ApplicationUser User { get; set; }

        [ForeignKey(nameof(GroupId))]
        public virtual Group Group { get; set; }
    }
}

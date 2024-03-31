using System.ComponentModel.DataAnnotations;

namespace AroundTheWorld_Persistence.Models
{
    public class Post
    {
        [Key]
        public string Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public DateOnly Date { get; set; }
        public TimeOnly Time { get; set; }
        public string UserId { get; set; }
    }
}

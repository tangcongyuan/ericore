using System.ComponentModel.DataAnnotations;

namespace Ericore.Entities
{
    public class Comment
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Comment")]
        public string Text { get; set; }
    }
}

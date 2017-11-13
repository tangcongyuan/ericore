using System.ComponentModel.DataAnnotations;

namespace Ericore.ViewModels
{
    public class CommentEditViewModel
    {
        [Required]
        [Display(Name = "Comment")]
        public string  Text { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ericore.ViewModels
{
    public class CommentEditViewModel
    {
        [Required]
        [Display(Name = "Comment")]
        public string  Text { get; set; }
    }
}

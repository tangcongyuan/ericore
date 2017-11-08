using Ericore.Entities;
using System.Collections.Generic;

namespace Ericore.ViewModels
{
    public class HomePageViewModel
    {
        public IEnumerable<Comment> Comments { get; set; }
        public string Greeting { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.FlashCard.User
{
    public class FlashCardViewModel
    {
        public int? Id { get; set; }
        public string? FrontContent { get; set; } = null!;
        public string? BackContent { get; set; } = null!;
        public int? LessonMethodId { get; set; }
    }
}

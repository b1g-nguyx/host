using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.Base;
using ViewModel.Region.User;

namespace ViewModel.Quitz.User
{
    public class QuitzViewModel
    {
        public int LessonId { get; set; }
        public List<QuizQuestionViewModel>? Question { get; set; } = new List<QuizQuestionViewModel>();
        public LeftSlideViewModel LeftSlideViewModel { get; set; } = new LeftSlideViewModel();
        public string? Title { get; set; }
    }
}

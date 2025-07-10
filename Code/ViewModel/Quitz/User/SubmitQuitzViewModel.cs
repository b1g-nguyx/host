using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.Quitz.User
{
    public class SubmitQuitzViewModel
    {
        public int LessonId { get; set; }
        public List<UserAnswerViewModel> UserAnswers { get; set; } = new List<UserAnswerViewModel>();
    }
}

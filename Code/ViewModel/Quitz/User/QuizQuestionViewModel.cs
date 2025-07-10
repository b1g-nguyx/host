using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.Base;

namespace ViewModel.Quitz.User
{
    public class QuizQuestionViewModel
    {
        public int QuestionId { get; set; }
        public string? QuestionContent { get; set; }
        public List<QuizAnswerViewModel> Answers { get; set; } = new List<QuizAnswerViewModel>();
    }
}

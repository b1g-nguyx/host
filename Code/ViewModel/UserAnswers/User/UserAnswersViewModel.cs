using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.UserAnswers.User
{
    public class UserAnswersViewModel
    {
        public int UserId { get; set; }
        public int QuestionId { get; set; }
        public string? Answer { get; set; } 
        public bool IsCorrect { get; set; } 
    }
}

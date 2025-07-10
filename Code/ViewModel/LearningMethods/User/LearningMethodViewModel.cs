using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.LearningMethods.User
{
    public class LearningMethodViewModel
    {
        public int Id { get; set; }
        public string? MethodName { get; set; } 
        public string? Description { get; set; }
        public string? ImageBase64 { get; set; }
    }
}

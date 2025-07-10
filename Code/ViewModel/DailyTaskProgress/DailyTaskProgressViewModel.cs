using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.DailyTaskProgress
{
    public class DailyTaskProgressViewModel
    {
        public string? FullName { get; set; }
        public int ExpProgress { get; set; } // % kinh nghiệm
        public int HighScoreProgress { get; set; } // % đạt >=90%
        public int StudyTimeProgress { get; set; } // % học >=10p
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.Base;

namespace ViewModel.Learn.User
{
    public class LearnHomeViewModel
    {
        public LearnViewModel learn { get; set; } = new LearnViewModel();
        public LeftSlideViewModel leftSlide { get; set; } = new LeftSlideViewModel();
    }
}

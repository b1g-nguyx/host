using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.Base;
using ViewModel.LearningMethods.User;

namespace ViewModel.Learn.User
{
    public class LearnDetailViewModel
    {
        public LearnViewModel Learn { get; set; } = new LearnViewModel();
        public LeftSlideViewModel LeftSlide { get; set; } = new LeftSlideViewModel();
        public List<LearningMethodViewModel> LearnMethod { get; set; } = new List<LearningMethodViewModel>();
    }
}

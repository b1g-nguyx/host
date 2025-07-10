using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.Base;

namespace ViewModel.Video.User
{
    public class VideoAllViewModel
    {
        public SearchViewModel Search { get; set; } = new SearchViewModel();
        public LeftSlideViewModel LeftSlide { get; set; } = new LeftSlideViewModel();
        public List<VideoViewModel> Video { get; set; } = new List<VideoViewModel>();
    }
}

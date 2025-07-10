using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.Base;
using ViewModel.Region.User;

namespace ViewModel.Video.User
{
    public class VideoDetailViewModel
    {
        public int LessonId { get; set; }
        public LeftSlideViewModel LeftSlide { get; set; } = new LeftSlideViewModel();
        public VideoViewModel Video { get; set; } = new VideoViewModel();
        
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.Base;

namespace ViewModel.Course.User
{
    public class CourseHomeViewModel
    {
      public  List<CourseViewModel> courses { get; set; } = new List<CourseViewModel>();
       public LeftSlideViewModel leftSlide { get; set; } = new LeftSlideViewModel();
    }
}

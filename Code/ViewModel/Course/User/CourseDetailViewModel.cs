using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.Base;
using ViewModel.Learn.User;
using ViewModel.Lesson.User;

namespace ViewModel.Course.User
{
    public class CourseDetailViewModel
    {
        public CourseViewModel course = new CourseViewModel();
        public LeftSlideViewModel LeftSlide = new LeftSlideViewModel();
        public List<LessonViewModel> Lesson = new List<LessonViewModel>();
    }
}
    
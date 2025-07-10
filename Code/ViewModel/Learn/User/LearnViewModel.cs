using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.Course.User;
using ViewModel.Lesson.User;

namespace ViewModel.Learn.User
{
    public class LearnViewModel
    {
        public LessonViewModel Lesson { get; set; } = new LessonViewModel();
        public CourseViewModel Course { get; set; } = new CourseViewModel();
    }
}

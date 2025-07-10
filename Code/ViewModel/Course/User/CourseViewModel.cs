using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.Course.User
{
    public class CourseViewModel
    {
        public int Id { get; set; }
        public string CourseName { get; set; } = null!;
        public string? Description { get; set; }
        public string? ImageBase64 { get; set; }
    }
}

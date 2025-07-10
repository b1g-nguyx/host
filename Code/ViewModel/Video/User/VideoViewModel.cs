using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.Video.User
{
    public class VideoViewModel
    {
        public int Id { get; set; } 
        public string? Title { get; set; } 
        public string? Description { get; set; } 
        public string? Url { get; set; } 
        public int? Duration { get; set; } 
        public int LessonMethodId { get; set; } 
        public string? Thumbnail { get; set; } // Đường dẫn đến hình thu nhỏ của video
    }
}

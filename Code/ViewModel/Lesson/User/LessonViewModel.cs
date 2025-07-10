
namespace ViewModel.Lesson.User
{
    public class LessonViewModel
    {
        public int Id { get; set; }
        public int? RegionID { get; set; }
        public string Title { get; set; } = null!;
        public string? Content { get; set; }
        public string? Source { get; set; }
        public string? Authors { get; set; }
        public string? Thumbnail { get; set; } // Hình ảnh đại diện cho bài học
        public int? CourseId { get; set; } 
    }
}

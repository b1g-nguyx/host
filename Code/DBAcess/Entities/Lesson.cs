using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBAcess.Entities
{
    // Lessons: Quản lý bài học lịch sử
    [Table("Lessons")]
    public class Lesson : IEntity<int>
    {
        [Key]
        public int Id { get; set; }

        public int? RegionID { get; set; }

        [Required]
        [MaxLength(200)]
        [Column(TypeName = "NVARCHAR")]
        public string Title { get; set; } = null!;
        public string? Thumbnail { get; set; } // Hình ảnh đại diện cho bài học

        [Column(TypeName = "NVARCHAR(MAX)")]
        public string? Content { get; set; }

        [Column(TypeName = "NVARCHAR(MAX)")]
        public string? Authors { get; set; }

        [Column(TypeName = "NVARCHAR(MAX)")]
        public string? Source { get; set; }

        public int? CourseId { get; set; } // <-- Thêm khóa ngoại tới Course

        [ForeignKey("RegionID")]
        public Region? Region { get; set; }

        [ForeignKey("CourseId")]
        public Course? Course { get; set; }

        public ICollection<Question> Questions { get; set; } = new List<Question>();
        public ICollection<LessonProgress> LessonProgresses { get; set; } = new List<LessonProgress>();
        public ICollection<LearningPathLesson> LearningPathLessons { get; set; } = new List<LearningPathLesson>();
    }
}

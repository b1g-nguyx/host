using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBAcess.Entities
{
    //Questions: Quản lý câu hỏi
    [Table("Questions")]
    public class Question : IEntity<int>
    {
        [Key]
        public int Id { get; set; }

        public int? LessonID { get; set; }

        [Required]
        public string Content { get; set; } = null!;

        [MaxLength(20)]
        public string? QuestionType { get; set; } // Trắc nghiệm, Tự luận

        [ForeignKey("LessonID")]
        public Lesson? Lesson { get; set; }
        public ICollection<Answer> Answers { get; set; } = new List<Answer>();
        public ICollection<UserAnswer> UserAnswers { get; set; } = new List<UserAnswer>();
    }
}

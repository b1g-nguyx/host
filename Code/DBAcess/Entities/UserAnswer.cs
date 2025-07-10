using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBAcess.Entities
{
    // UserAnswers: Lưu câu trả lời của người dùng
    [Table("UserAnswers")]
    public class UserAnswer : IEntity<int>
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UserID { get; set; } // Liên kết với Users

        [Required]
        public int QuestionID { get; set; }
        [Column(TypeName = "NVARCHAR(MAX)")]
        public string? AnswerText { get; set; }

        public bool IsCorrect { get; set; }

        [ForeignKey("UserID")]
        public User? User { get; set; }

        [ForeignKey("QuestionID")]
        public Question? Question { get; set; }
    }
}
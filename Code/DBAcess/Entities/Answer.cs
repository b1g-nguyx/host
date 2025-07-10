using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBAcess.Entities
{
    [Table("Answers")]

    public class Answer : IEntity<int>
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int QuestionId { get; set; } // Liên kết với Question
        public bool IsCorrect { get; set; } = false;// Trả lời đúng hay sai
        [Required]
        [Column(TypeName = "NVARCHAR(MAX)")]
        public string Content { get; set; } = null!; // Nội dung câu trả lời
        [ForeignKey("QuestionId")]

        public Question? Question { get; set; } // Liên kết với câu hỏi
    }
}

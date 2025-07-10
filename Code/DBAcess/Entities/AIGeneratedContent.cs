using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBAcess.Entities
{
    //AIGeneratedContent: Quản lý nội dung do AI tạo
    [Table("AIGeneratedContent")]
    public class AIGeneratedContent : IEntity<int>
    {
        [Key]
        public int Id { get; set; }

        public int UserId { get; set; } // Liên kết với Users
        [MaxLength(50)]
        [Column(TypeName = "NVARCHAR")]
        public string? ContentType { get; set; } // Lộ trình, Câu hỏi, Bài viết
        [Column(TypeName = "NVARCHAR(MAX)")]
        public string? ContentData { get; set; }
        [Column(TypeName = "NVARCHAR(MAX)")]
        public string? Prompt { get; set; }
        [Column(TypeName = "NVARCHAR(MAX)")]
        public string? Response { get; set; } 
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; } = null!;
    }
}

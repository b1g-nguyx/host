using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBAcess.Entities
{
    //BlogPosts: Quản lý bài viết blog
    [Table("BlogPosts")]
    public class BlogPost : IEntity<int>
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(450)]
        public string? UserId { get; set; } // Liên kết với ApplicationUser (cả Admin và User đều có thể viết blog)

        [Required]
        [MaxLength(200)]
        [Column(TypeName = "NVARCHAR")]
        public string? Title { get; set; }
        [Column(TypeName = "NVARCHAR(MAX)")]
        public string? Content { get; set; }

        public DateTime CreatedDate { get; set; }

        [ForeignKey("UserId")]
        public ApplicationUser? User { get; set; }
    }
}

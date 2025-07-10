using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBAcess.Entities
{
    [Table("Videos")]
    public class Video
    {
        public int Id { get; set; } // Khóa chính
        [Column(TypeName = "NVARCHAR(MAX)")]
        public string? Title { get; set; } // Tiêu đề video
        [Column(TypeName = "NVARCHAR(MAX)")]
        public string? Description { get; set; } // Mô tả video
        public string? Url { get; set; } // Đường dẫn đến video
        public int? Duration { get; set; } // Thời lượng video tính bằng giây
        public string? Thumbnail { get; set; } // Đường dẫn đến hình thu nhỏ của video
        [ForeignKey("LessonMethodId")]
        public int LessonMethodId { get; set; } // Khóa ngoại tới LessonMethod
        public LessonMethod? LessonMethods { get; set; } // Liên kết với LearningMethod
    }
}

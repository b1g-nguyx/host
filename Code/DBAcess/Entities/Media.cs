using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBAcess.Entities
{
    //Quản lý ảnh, video, tài liệu
    [Table("Media")]
    public class Media : IEntity<int>
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(256)]
        public string? FilePath { get; set; }
            
        [MaxLength(20)]
        public string? MediaType { get; set; } // Ảnh, Video, Tài liệu

        public int? RelatedID { get; set; }

        [MaxLength(20)]
        public string? RelatedType { get; set; } // Lesson, BlogPost, Course

        public DateTime UploadedDate { get; set; }
    }
}

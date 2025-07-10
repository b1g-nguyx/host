using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBAcess.Entities
{
    //Courses: Quản lý khóa học
    [Table("Courses")]
    public class Course : IEntity<int>
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        [Column(TypeName = "NVARCHAR")]
        public string CourseName { get; set; } = null!;
        [Column(TypeName = "NVARCHAR(MAX)")]
        public string? Description { get; set; }

        public DateTime CreatedDate { get; set; }

        public string? ImageBase64 { get; set; }
        public ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();
    }
}

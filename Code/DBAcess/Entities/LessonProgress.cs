using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBAcess.Entities
{
    //LessonProgress: Theo dõi tiến độ học tập của người dùng
    [Table("LessonProgress")]
    public class LessonProgress : IEntity<int>
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UserID { get; set; } // Liên kết với Users

        [Required]
        public int LessonID { get; set; }

        [MaxLength(20)]
        public string Status { get; set; } = null!; // Hoàn thành, Đang học, Chưa học

        public int ProgressPercentage { get; set; }

        [ForeignKey("UserID")]
        public User User { get; set; } = null!;

        [ForeignKey("LessonID")]
        public Lesson Lesson { get; set; } = null!;
    }
}

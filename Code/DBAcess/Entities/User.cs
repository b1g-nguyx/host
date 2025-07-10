using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBAcess.Entities
{
    //Users: Thông tin bổ sung của User (thay thế UserProfiles)
    [Table("Users")]
    public class User : IEntity<int>
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(450)]
        public string UserId { get; set; } = null!;

        [Column(TypeName = "NVARCHAR")]
        [MaxLength(100)]
        public string? FullName { get; set; }

        [Column(TypeName = "NVARCHAR")]
        [MaxLength(100)]
        public string? SchoolName { get; set; }

        [MaxLength(256)]
        public string? VerificationDocument { get; set; } // Đường dẫn ảnh thẻ hoặc giấy tờ

        [MaxLength(20)]
        public string? UserType { get; set; } // Học sinh, Sinh viên, Người đi làm

        [ForeignKey("UserId")]
        public ApplicationUser ApplicationUser { get; set; } = null!;
    }
}

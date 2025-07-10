using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBAcess.Entities
{
    //Admins: Thông tin bổ sung của Admin
    [Table("Admins")]
    public class Admin : IEntity<int>
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(450)]
        public string? UserId { get; set; }

        [MaxLength(100)]
        [Column(TypeName = "NVARCHAR")]
        public string? FullName { get; set; }

        [MaxLength(50)]
        public string? AdminRole { get; set; } // Ví dụ: SuperAdmin, Moderator

        [ForeignKey("UserId")]
        public ApplicationUser? ApplicationUser { get; set; }
    }
}

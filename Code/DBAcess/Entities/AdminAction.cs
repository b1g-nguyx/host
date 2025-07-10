using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBAcess.Entities
{
    //AdminActions: Quản lý thao tác của admin
    [Table("AdminActions")]
    public class AdminAction : IEntity<int>
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int AdminID { get; set; } // Liên kết với Admins

        [MaxLength(50)]
        [Column(TypeName = "NVARCHAR")]
        public string? ActionType { get; set; }
        [Column(TypeName = "NVARCHAR(MAX)")]
        public string? Description { get; set; }

        public DateTime ActionDate { get; set; }

        [ForeignKey("AdminID")]
        public Admin? Admin { get; set; }
    }
}

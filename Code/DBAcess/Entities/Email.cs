using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBAcess.Entities
{
    //Emails: Quản lý email
    [Table("Emails")]
    public class Email : IEntity<int>
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UserID { get; set; } // Liên kết với Users

        [MaxLength(200)]
        public string? Subject { get; set; }
        [Column(TypeName = "NVARCHAR(MAX)")]
        public string? Content { get; set; }

        public DateTime? SentDate { get; set; }

        [ForeignKey("UserID")]
        public User? User { get; set; }
    }
}

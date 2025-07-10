using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBAcess.Entities
{
    //UserVIPSubscriptions: Quản lý đăng ký VIP
    [Table("UserVIPSubscriptions")]
    public class UserVIPSubscription : IEntity<int>
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UserID { get; set; } // Liên kết với Users, không phải ApplicationUser

        [Required]
        public int VIPLevelID { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        [ForeignKey("UserID")]
        public User? User { get; set; }

        [ForeignKey("VIPLevelID")]
        public VIPLevel? VIPLevel { get; set; }
    }
}

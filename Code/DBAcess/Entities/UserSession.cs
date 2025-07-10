using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBAcess.Entities
{
    [Table("UserSessions")]
    public class UserSession: IEntity<int>
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; } = null!; 
        public DateTime LoginTime { get; set; }
        public DateTime? LogoutTime { get; set; }
        public string? IPAddress { get; set; }
        public string? UserAgent { get; set; }

        public virtual ApplicationUser User { get; set; } = null!;
    }
}

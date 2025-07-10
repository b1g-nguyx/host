using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBAcess.Entities
{
    //VIPLevels: Quản lý các cấp độ VIP
    [Table("VIPLevels")]
    public class VIPLevel : IEntity<int>
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        [Column(TypeName = "NVARCHAR")]

        public string? LevelName { get; set; }

        [MaxLength(500)]
        [Column(TypeName = "NVARCHAR")]

        public string? Description { get; set; }
        public int DurationInDays { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBAcess.Entities
{
    //HistoricalSites: Quản lý di tích lịch sử
    [Table("HistoricalSites")]
    public class HistoricalSite : IEntity<int>
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int RegionID { get; set; }

        [Required]
        [MaxLength(200)]
        [Column(TypeName = "NVARCHAR")]
        public string? SiteName { get; set; }
        [Column(TypeName = "NVARCHAR(MAX)")]
        public string? Description { get; set; }
        public string? linkImage360 { get; set; }

        [ForeignKey("RegionID")]
        public Region? Region { get; set; }
    }
}

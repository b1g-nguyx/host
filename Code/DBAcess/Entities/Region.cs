using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBAcess.Entities
{
    //Regions: Quản lý khu vực địa lý
    [Table("Regions")]
    public class Region : IEntity<int>
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        [Column(TypeName = "NVARCHAR")]
        public string RegionName { get; set; } = null!;
        [Column(TypeName = "NVARCHAR")]
        [MaxLength(500)]
        public string? Description { get; set; }
        [MaxLength(200)]
        public string? Link { get; set; }
        [MaxLength(100)]
        public string? MapId { get; set; }

        public ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();
        public ICollection<HistoricalSite> HistoricalSites { get; set; } = new List<HistoricalSite>();
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBAcess.Entities
{

    [Table("Vouchers")]
    public class Voucher : IEntity<int>
    {
        [Key]
        public int Id { get; set; }

        public string? Code { get; set; }

        [Column(TypeName = "NVARCHAR(MAX)")]
        public string? Description { get; set; }
        public decimal? DiscountAmount { get; set; }
        public int? DiscountPercent { get; set; }

        public int MaxUsage { get; set; }
        public int UsedCount { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public bool IsActive { get; set; }

    }

}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBAcess.Entities
{

    [Table("UserVouchers")]

    public class UserVoucher : IEntity<int>
    {
        [Key]
        public int Id { get; set; }

        public int? UserId { get; set; }
        public User? User { get; set; } = null!;

        public int? VoucherId { get; set; }
        public Voucher? Voucher { get; set; } = null!;

        public DateTime RedeemedAt { get; set; }
        public bool IsUsed { get; set; }
    }

}

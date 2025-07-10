using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBAcess.Entities
{
    [Table("LearningMethods")]
    public class LearningMethod
    {
        public int Id { get; set; }
        [Column(TypeName = "NVARCHAR(MAX)")]
        public string? MethodName { get; set; }
        [Column(TypeName = "NVARCHAR(MAX)")]
        public string? Description { get; set; } 
        public string? LinkImage { get; set; } 
    }
}

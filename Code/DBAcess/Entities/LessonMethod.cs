using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBAcess.Entities
{
    [Table("LessonMethods")]
    public class LessonMethod
    {
        public int Id { get; set; }
        public int? LessonId { get; set; }
        public int? LearningMethodId { get; set; }
      
        [ForeignKey("LearningMethodId")]
        public LearningMethod? LearningMethod { get; set; }

        [ForeignKey("LessonId")]
        public Lesson? Lesson { get; set; }
    }
}

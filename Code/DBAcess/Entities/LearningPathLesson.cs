using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBAcess.Entities
{
    //LearningPathLessons: Liên kết giữa LearningPaths và Lessons
    [Table("LearningPathLessons")]
    public class LearningPathLesson
    {
        public int PathID { get; set; }
        public int LessonID { get; set; }

        [ForeignKey("PathID")]
        public LearningPath LearningPath { get; set; } = null!;

        [ForeignKey("LessonID")]
        public Lesson Lesson { get; set; } = null!;
    }
}

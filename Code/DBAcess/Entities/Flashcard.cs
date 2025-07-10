using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBAcess.Entities
{
    [Table("Flashcards")]
    public class Flashcard
    {
        public int? Id { get; set; }
        [Column(TypeName = "NVARCHAR(MAX)")]
        public string? FrontContent { get; set; } = null!;
        [Column(TypeName = "NVARCHAR(MAX)")]
        public string? BackContent { get; set; } = null!;
        public int? LessonMethodId { get; set; }
        [ForeignKey("LessonMethodId")]
        public LessonMethod? LessonMethod { get; set; }
    }
}

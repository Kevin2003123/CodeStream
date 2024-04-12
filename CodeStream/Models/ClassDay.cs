using System.ComponentModel.DataAnnotations;

namespace CodeStream.Models
{
    public class ClassDay
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int ClassWeekId { get; set; }
        [Required]
        public TimeSpan? StartHour { get; set; } = null;
        [Required]
        public TimeSpan? EndHour { get; set; } = null;
        [Required]
        public int CourseId { get; set; }


    }
}

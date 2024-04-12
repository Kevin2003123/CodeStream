using System.ComponentModel.DataAnnotations;

namespace CodeStream.Models
{
    public class CourseSkill
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int CourseId { get; set; }
        [Required]
        public int SkillId { get; set; }
    }
}

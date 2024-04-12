using System.ComponentModel.DataAnnotations;

namespace CodeStream.Models.DTO
{
    public class CourseListDTO
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; } = decimal.Zero;
        public string Duration { get; set; } = string.Empty;
        public int TeacherId { get; set; } 
        public string TeacherName { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
        public string Level { get; set; } = string.Empty;
    }
}

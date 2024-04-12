using System.ComponentModel.DataAnnotations;

namespace CodeStream.Models.DTO
{
    public class CourseDetailtDTO
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; } = decimal.Zero;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Description2 { get; set; } = string.Empty;
        public string Duration { get; set; } = string.Empty;
        public string TeacherId { get; set; } = string.Empty;
        public string TeacherName { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
        public string Level { get; set; } = string.Empty;
    }
}

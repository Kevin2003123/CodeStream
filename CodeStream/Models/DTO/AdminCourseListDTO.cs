using System.ComponentModel.DataAnnotations;

namespace CodeStream.Models.DTO
{
    public class AdminCourseListDTO
    {
            [Key]
            public int Id { get; set; }
            public string Name { get; set; } = string.Empty;
            public decimal Price { get; set; } = decimal.Zero;
            public string Duration { get; set; } = string.Empty;
            public string TeacherName { get; set; } = string.Empty;
            public string Image { get; set; } = string.Empty;
            public string Level { get; set; } = string.Empty;
            public bool Active { get; set; }
        
    }
}

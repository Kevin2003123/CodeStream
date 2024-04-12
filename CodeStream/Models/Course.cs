using System.ComponentModel.DataAnnotations;

namespace CodeStream.Modelos
{
    public class Course
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; } = decimal.Zero;
        public DateOnly StartDate { get; set; } 
        public DateOnly EndDate { get; set; }
        public int TeacherId { get; set; }
        public string Image { get; set; } = string.Empty;
    }
}

using System.ComponentModel.DataAnnotations;

namespace CodeStream.Models.DTO
{
    public class FormCourseDTO
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; } = string.Empty;
        [Required]
        [StringLength(200)]
        public string Description { get; set; } = string.Empty;
            
        public string? Description2 { get; set; } = string.Empty;

        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "The field Price must be More than 0 ")]
        public decimal Price { get; set; } = decimal.Zero;
        [Required]
        public DateTime StartDate { get; set; } = DateTime.Now;
        [Required]
        public DateTime EndDate { get; set; } = DateTime.Now;
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "The field Level is required")]
        public int CourseLevelId { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "The field Duration is required")]
        public int CourseDurationId { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "The field Teacher is required")]
        public int TeacherId { get; set; }
        [Required]
        public string Image { get; set; } = string.Empty;
    }
}

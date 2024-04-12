using System.ComponentModel.DataAnnotations;

namespace CodeStream.Models.DTO
{
    public class TeacherDetailDTO
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string Phone { get; set; } = string.Empty;
        [Required]
        public string Description { get; set; } = string.Empty;
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        public string? Facebook { get; set; } = string.Empty;
        public string? LinkedIn { get; set; } = string.Empty;
        public string? X { get; set; } = string.Empty;
        public string? Instagram { get; set; } = string.Empty;
        public string? Youtube { get; set; } = string.Empty;
    }
}

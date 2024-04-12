using System.ComponentModel.DataAnnotations;

namespace CodeStream.Models.DTO
{
    public class AdminStudentListDTO
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string Phone { get; set; } = string.Empty;
        [Required]
        public string Email { get; set; } = string.Empty;
        [Required]
        public bool Active { get; set; }
        [Required]
        public int UserId { get; set; }

    }
}

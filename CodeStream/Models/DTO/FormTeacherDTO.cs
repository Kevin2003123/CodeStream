using System.ComponentModel.DataAnnotations;

namespace CodeStream.Models.DTO
{
    public class FormTeacherDTO
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Name field must have 50 characters max")]
        public string Name { get; set; } = string.Empty;
        [Required]
        //[RegularExpression(@"^\d{10}$", ErrorMessage = "Phone fiel must have 10 digits")]
        public string Phone { get; set; } = string.Empty;
        [Required]
        public string Description { get; set; } = string.Empty;
  
        [Required]
        [EmailAddress]

        public string Email { get; set; } = string.Empty;

        [Required]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "the password must have at least 6 characters.")]
        public string Password { get; set; } = string.Empty;


        public string? Facebook { get; set; } = string.Empty;
        public string? LinkedIn { get; set; } = string.Empty;
        public string? X { get; set; } = string.Empty;
        public string? Instagram { get; set; } = string.Empty;
        public string? Youtube { get; set; } = string.Empty;



    }
}

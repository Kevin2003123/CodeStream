using System.ComponentModel.DataAnnotations;

namespace CodeStream.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;

        [Required]
        public int RolId { get; set; }

    }
}

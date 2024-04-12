using System.ComponentModel.DataAnnotations;

namespace CodeStream.Models.DTO
{
    public class AdminSkillListDTO
    {

        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public bool Active { get; set; }
    }
}

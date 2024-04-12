using System.ComponentModel.DataAnnotations;

namespace CodeStream.Models.DTO
{
    public class GetSelectListTeacherDTO
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
    }
}

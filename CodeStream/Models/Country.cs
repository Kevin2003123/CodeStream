using System.ComponentModel.DataAnnotations;

namespace CodeStream.Models
{
    public class Country
    {
        [Key] 
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;
    }
}

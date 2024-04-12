using System.ComponentModel.DataAnnotations;

namespace CodeStream.Models
{
    public class WeekDay
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}

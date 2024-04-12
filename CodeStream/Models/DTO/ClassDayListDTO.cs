using System.ComponentModel.DataAnnotations;

namespace CodeStream.Models.DTO
{
    public class ClassDayListDTO
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int ClassWeekId { get; set; } 
        [Required]
        public TimeSpan StartHour { get; set; }
        [Required]
        public TimeSpan EndHour { get; set; }
       
    }
}

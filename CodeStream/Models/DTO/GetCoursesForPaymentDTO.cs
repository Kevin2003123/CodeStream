using System.ComponentModel.DataAnnotations;

namespace CodeStream.Models.DTO
{
    public class GetCoursesForPaymentDTO
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set;}
        [Required]
        public Decimal Price  { get; set;}
        [Required]
        public string Description { get; set;}
    }
}

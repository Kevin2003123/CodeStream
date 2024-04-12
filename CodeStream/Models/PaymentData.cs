using System.ComponentModel.DataAnnotations;

namespace CodeStream.Models
{
    public class PaymentData
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public int CountryId { get; set; }
        [Required]
        public decimal TotalPrice { get; set; }

    }
}

using System.ComponentModel.DataAnnotations;

namespace CodeStream.Models.DTO
{
    public class CreatePaymentDTO
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public int CountryId { get; set; }
        [Required] 
        public decimal TotalPrice { get; set; }

    }
}

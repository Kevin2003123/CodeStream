using System.ComponentModel.DataAnnotations;

namespace CodeStream.Models.DTO
{
    public class AdminPaymentListDTO
    {
            [Key]
            public int Id { get; set; }
            public int UserId { get; set; }
            public int StudentId { get; set; }
            public string Name { get; set; } = string.Empty;
            public string Email { get; set; } = string.Empty;
            public string Phone { get; set; } = string.Empty;
            public string Country { get; set; } = string.Empty;
            public decimal TotalPrice { get; set; } = decimal.Zero;
            public DateTime Date { get; set; }
        
    }
}

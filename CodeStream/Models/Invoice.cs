using Microsoft.Identity.Client;

namespace CodeStream.Models
{
    public class Invoice
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public decimal TotalPrice { get; set; }
        public string Country { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;

    }
}

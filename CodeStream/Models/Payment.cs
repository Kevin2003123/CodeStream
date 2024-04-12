using System.ComponentModel.DataAnnotations;
using System.Timers;

namespace CodeStream.Models
{
    public class Payment
    {

        [Required(ErrorMessage = "El país es obligatorio")]
        [Range(1, int.MaxValue, ErrorMessage = "Selecciona el pais donde te encuentras")]
        public int CountryId { get; set; }

        [Required(ErrorMessage = "El nombre en la tarjeta es obligatorio")]
        [RegularExpression(@"^[a-zA-Z ]+$", ErrorMessage = "El nombre en la tarjeta es obligatorio y solo puede contener letras")]
        public string CardName { get; set; } = string.Empty;

        [Required(ErrorMessage = "El número de la tarjeta es obligatorio")]
        [RegularExpression(@"^\d{4} \d{4} \d{4} \d{4}$", ErrorMessage = "El número de tarjeta debe tener el formato '0000 0000 0000 0000'")]
        public string CardNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "El CVC es obligatorio")]
        [RegularExpression(@"^\d{3}$", ErrorMessage = "El CVC debe ser un número de 3 dígitos")]
        public string CVC { get; set; } = string.Empty;

        [Required(ErrorMessage = "La fecha de expiración es obligatoria")]
        [RegularExpression(@"^(0[1-9]|1[0-2])\/[0-9]{2}$", ErrorMessage = "La fecha de expiración debe tener el formato 'MM/AA'")]
        public string ExpirationDate { get; set; } = string.Empty;

    }
}

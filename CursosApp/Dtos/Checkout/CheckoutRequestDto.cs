using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CursosApp.Dtos.Checkout
{
    public class CheckoutRequestDto
    {

        [Display(Name = "Número de tarjeta")]
        [Required(ErrorMessage = "El {0} es requerido.")]
        [CreditCard(ErrorMessage = "El número de tarjeta no es válido.")]
        public string CardNumber { get; set; }

        [Display(Name = "Vencimiento")]
        [Required(ErrorMessage = "El {0} es requerido.")]
        [RegularExpression(@"^(0[1-9]|1[0-2])\/\d{2}$", ErrorMessage = "Usa el formato MM/AA.")]
        public string CardExpiry { get; set; }

        [Display(Name = "CVV")]
        [Required(ErrorMessage = "El {0} es requerido.")]
        [RegularExpression(@"^\d{3,4}$", ErrorMessage = "El CVV debe tener 3 o 4 dígitos.")]
        public string CardCvv { get; set; }

        [Required(ErrorMessage = "El carrito no puede estar vacío.")]
        [MinLength(1, ErrorMessage = "Debes agregar al menos un curso.")]
        public List<CheckoutItemDto> Items { get; set; }
    }
}
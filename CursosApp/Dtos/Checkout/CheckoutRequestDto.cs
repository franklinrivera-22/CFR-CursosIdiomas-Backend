using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CursosApp.Dtos.Checkout
{
    public class CheckoutRequestDto
    {
        [Required(ErrorMessage = "El carrito no puede estar vacío.")]
        [MinLength(1, ErrorMessage = "Debes agregar al menos un curso.")]
        public List<CheckoutItemDto> Items { get; set; }
    }
}
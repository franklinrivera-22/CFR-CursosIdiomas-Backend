
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CursosApp.Dtos.Checkout
{
    public class CheckoutItemDto
    {
        [Required(ErrorMessage = "El curso es requerido.")]
        public string CourseId { get; set; }

        [Range(1, 20, ErrorMessage = "La cantidad debe estar entre 1 y 20.")]
        public int Quantity { get; set; } = 1;
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CursosApp.Dtos.Auth
{
    public class LoginDto
    {
        [Display(Name = "Correo electrónico")]
        [Required(ErrorMessage = "El {0} es requerido.")]
        [EmailAddress(ErrorMessage = "El {0} no tiene un formato válido.")]
        public string Email { get; set; }

        [Display(Name = "Contraseña")]
        [Required(ErrorMessage = "La {0} es requerida.")]
        public string Password { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;

namespace CursosApp.Dtos.Auth
{
    public class RegisterDto
    {
        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "El {0} es requerido.")]
        [StringLength(60, MinimumLength = 2, ErrorMessage = "El {0} debe tener entre {2} y {1} caracteres.")]
        public string FirstName { get; set; }

        [Display(Name = "Apellido")]
        [Required(ErrorMessage = "El {0} es requerido.")]
        [StringLength(60, MinimumLength = 2, ErrorMessage = "El {0} debe tener entre {2} y {1} caracteres.")]
        public string LastName { get; set; }

        [Display(Name = "Correo electrónico")]
        [Required(ErrorMessage = "El {0} es requerido.")]
        [EmailAddress(ErrorMessage = "El {0} no tiene un formato válido.")]
        public string Email { get; set; }

        [Display(Name = "Contraseña")]
        [Required(ErrorMessage = "La {0} es requerida.")]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "La {0} debe tener al menos {2} caracteres.")]
        public string Password { get; set; }

        [Display(Name = "Confirmación de contraseña")]
        [Compare(nameof(Password), ErrorMessage = "Las contraseñas no coinciden.")]
        public string ConfirmPassword { get; set; }
    }
}
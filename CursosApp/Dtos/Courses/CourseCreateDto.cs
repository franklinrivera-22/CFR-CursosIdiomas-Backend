using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CursosApp.Dtos.Courses
{
    public class CourseCreateDto
    {
     [Display(Name = "Título")]
        [Required(ErrorMessage = "El {0} es requerido.")]
        [StringLength(120, ErrorMessage = "El {0} debe tener entre {2} y {1} caracteres.", MinimumLength = 3)]
        public string Title { get; set; }

        [Display(Name = "Descripción")]
        [Required(ErrorMessage = "La {0} es requerida.")]
        [StringLength(1000, ErrorMessage = "La {0} no puede superar {1} caracteres.")]
        public string Description { get; set; }

        [Display(Name = "Nivel")]
        [Required(ErrorMessage = "El {0} es requerido.")]
        [RegularExpression("^(A1|A2|B1|B2|C1|C2)$", ErrorMessage = "El nivel debe ser A1, A2, B1, B2, C1 o C2.")]
        public string Level { get; set; }

        [Display(Name = "Precio")]
        [Range(0, 100000, ErrorMessage = "El {0} debe ser un valor positivo.")]
        public decimal Price { get; set; }

        [Display(Name = "Duración (horas)")]
        [Range(1, 1000, ErrorMessage = "La {0} debe ser mayor a 0.")]
        public int DurationHours { get; set; }

        [Display(Name = "Imagen")]
        public string ImageUrl { get; set; }

        public bool IsActive { get; set; } = true;

        [Display(Name = "Categoría")]
        [Required(ErrorMessage = "La {0} es requerida.")]
        public string CategoryId { get; set; }
    }
}
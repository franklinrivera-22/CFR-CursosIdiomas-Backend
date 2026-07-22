
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CursosApp.Entities
{
    [Table("categories")]
    public class CategoryEntity : BaseEntity
    {
        [Required]
        [StringLength(60)]
        [Column("name")]
        public string Name { get; set; }

        [StringLength(200)]
        [Column("description")]
        public string Description { get; set; }

   
        public virtual IEnumerable<CourseEntity> Courses { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace CursosApp.Entities
{
    [Table("courses")]
    public class CourseEntity : BaseEntity
    {
        [Required]
        [StringLength(120)]
        [Column("title")]
        public string Title { get; set; }

        [Required]
        [StringLength(1000)]
        [Column("description")]
        public string Description { get; set; }

        [Required]
        [StringLength(2)]
        [Column("level")]
        public string Level { get; set; }

        [Column("price")]
        public decimal Price { get; set; }

        [Column("duration_hours")]
        public int DurationHours { get; set; }

        [Column("image_url")]
        public string ImageUrl { get; set; }

        [Column("is_active")]
        public bool IsActive { get; set; }

        [Required]
        [Column("category_id")]
        public string CategoryId { get; set; }

        [ForeignKey(nameof(CategoryId))]
        public virtual CategoryEntity Category { get; set; }
    }
}
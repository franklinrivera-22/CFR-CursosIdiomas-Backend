using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CursosApp.Entities
{
    [Table("enrollments")]
    public class EnrollmentEntity : BaseEntity
    {
        [Required]
        [Column("user_id")]
        public string UserId { get; set; }

        [Required]
        [Column("course_id")]
        public string CourseId { get; set; }

        [ForeignKey(nameof(CourseId))]
        public virtual CourseEntity Course { get; set; }

        [Column("transaction_id")]
        public string TransactionId { get; set; }

        [Column("progress")]
        public int Progress { get; set; }

        [Column("is_active")]
        public bool IsActive { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CursosApp.Entities
{
    [Table("transaction_items")]
    public class TransactionItemEntity : BaseEntity
    {
        [Required]
        [Column("transaction_id")]
        public string TransactionId { get; set; }

        [ForeignKey(nameof(TransactionId))]
        public virtual TransactionEntity Transaction { get; set; }

        [Required]
        [Column("course_id")]
        public string CourseId { get; set; }

        [Column("course_title")]
        public string CourseTitle { get; set; }

        [Column("unit_price")]
        public decimal UnitPrice { get; set; }

        [Column("quantity")]
        public int Quantity { get; set; }
    }
}
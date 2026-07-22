using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CursosApp.Entities
{
    [Table("transactions")]
    public class TransactionEntity : BaseEntity
    {
        
        [Column("user_id")]
        public string UserId { get; set; }

        [Required]
        [StringLength(120)]
        [Column("customer_name")]
        public string CustomerName { get; set; }

        [Required]
        [StringLength(256)]
        [Column("customer_email")]
        public string CustomerEmail { get; set; }

        [Column("amount")]
        public decimal Amount { get; set; }

        [Required]
        [StringLength(20)]
        [Column("status")]
        public string Status { get; set; }

        [Column("payment_reference")]
        public string PaymentReference { get; set; }

        [Column("payment_message")]
        public string PaymentMessage { get; set; }

        public virtual ICollection<TransactionItemEntity> Items { get; set; }
    }
}
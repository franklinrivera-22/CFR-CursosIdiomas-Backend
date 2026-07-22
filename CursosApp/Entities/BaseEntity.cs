using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace CursosApp.Entities
{
    public class BaseEntity
    {
        [Key]
        [Column("id")]
        public string Id { get; set; }
   
        [Column("created_by_id")]
        public string CreatedById { get; set; }

        [Column("created_date")]
        public DateTime CreatedDate { get; set; }

        [Column("updated_by_id")]
        public string UpdatedById { get; set; }

        [Column("updated_date")]
        public DateTime UpdatedDate { get; set; }
    }
}
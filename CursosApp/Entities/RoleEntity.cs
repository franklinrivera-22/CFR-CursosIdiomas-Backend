using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace CursosApp.Entities
{
    public class RoleEntity : IdentityRole
    {
        [Column("description")]
        public string Description { get; set; }
    }
}
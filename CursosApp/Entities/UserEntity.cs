using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace CursosApp.Entities
{
    public class UserEntity : IdentityUser
    {
        [Required]
        [Column("first_name")]
        public string FirstName { get; set; }

        [Required] 
        [Column("last_name")]
        public string LastName { get; set; }

        [Column("refresh_token")]
        public string RefreshToken { get; set; }

        [Column("refresh_token_expiry")]
        public DateTime RefreshTokenExpiry { get; set; }
    }
}
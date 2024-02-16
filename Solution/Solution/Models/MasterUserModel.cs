using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Solution.Models
{
    [Table("MasterUsers")]
    public class MasterUserModel : IdentityUser
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        [StringLength(250, MinimumLength = 3)]
        public string? Email { get; set; }

        [StringLength(250, MinimumLength = 3)]
        public string? PasswordHash { get; set; }
        [StringLength(250, MinimumLength = 3)]
        public bool IsActive { get; set; } = false;
    }
}

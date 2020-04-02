using System.ComponentModel.DataAnnotations;

namespace Tudou.Abp.Identity
{
    public class IdentityUserUpdateRolesDto
    {
        [Required]
        public string[] RoleNames { get; set; }
    }
}
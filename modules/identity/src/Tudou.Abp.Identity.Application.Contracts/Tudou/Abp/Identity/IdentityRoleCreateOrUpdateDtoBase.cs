using System.ComponentModel.DataAnnotations;

namespace Tudou.Abp.Identity
{
    public class IdentityRoleCreateOrUpdateDtoBase
    {
        [Required]
        [StringLength(IdentityRoleConsts.MaxNameLength)]
        public string Name { get; set; }

        public bool IsDefault { get; set; }

        public bool IsPublic { get; set; }
    }
}
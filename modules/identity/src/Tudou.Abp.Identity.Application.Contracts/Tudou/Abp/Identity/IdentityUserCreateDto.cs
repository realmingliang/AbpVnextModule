using System.ComponentModel.DataAnnotations;
using Volo.Abp.Auditing;

namespace Tudou.Abp.Identity
{
    public class IdentityUserCreateDto : IdentityUserCreateOrUpdateDtoBase
    {
        [Required]
        [StringLength(IdentityUserConsts.MaxPasswordLength)]
        [DisableAuditing]
        public string Password { get; set; }
    }
}
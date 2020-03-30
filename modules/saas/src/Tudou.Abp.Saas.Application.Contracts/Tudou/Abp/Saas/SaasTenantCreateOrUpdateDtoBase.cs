using System.ComponentModel.DataAnnotations;

namespace Tudou.Abp.Saas
{
    public abstract class SaasTenantCreateOrUpdateDtoBase
    {
        [Required]
        [StringLength(SaasTenantConsts.MaxNameLength)]
        public string Name { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;

namespace Tudou.Abp.Identity.OrganizationUnits
{
    public class UpdateOrganizationUnitInput
    {
        [Required]
        [StringLength(OrganizationUnitConsts.MaxDisplayNameLength)]
        public string DisplayName { get; set; }
    }
}
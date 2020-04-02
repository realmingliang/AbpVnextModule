using System;
using System.ComponentModel.DataAnnotations;

namespace Tudou.Abp.Identity.OrganizationUnits
{
    public class CreateOrganizationUnitInput
    {
        public Guid? ParentId { get; set; }

        [Required]
        [StringLength(OrganizationUnitConsts.MaxDisplayNameLength)]
        public string DisplayName { get; set; }
    }
}
using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace Tudou.Abp.OrganizationUnit
{
    public class UpdateOrganizationUnitInput 
    {
        [StringLength(OrganizationUnitConsts.MaxNameLength)]
        public string Name { get; set; }
    }
}
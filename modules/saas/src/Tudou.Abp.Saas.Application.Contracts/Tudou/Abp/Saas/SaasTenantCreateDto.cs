using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Tudou.Abp.Saas.Localization;

namespace Tudou.Abp.Saas
{
    public class SaasTenantCreateDto : SaasTenantCreateOrUpdateDtoBase
    {
        [Required]
        [EmailAddress]
        [MaxLength(256)]
        public string AdminEmailAddress { get; set; }

        public Guid? EditionId { get; set; }
        [Required]
        [MaxLength(128)]
        public string AdminPassword { get; set; }
    }
}
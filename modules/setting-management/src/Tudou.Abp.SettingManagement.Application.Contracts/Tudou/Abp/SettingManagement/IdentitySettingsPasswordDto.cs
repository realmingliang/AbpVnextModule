using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Tudou.Abp.SettingManagement
{
   public class IdentitySettingsPasswordDto
    {
        [MaxLength(128)]
        [MinLength(2)]
        public int RequiredLength { get; set; }
        [MaxLength(128)]
        [MinLength(1)]
        public int RequiredUniqueChars { get; set; }
        public bool RequireNonAlphanumeric { get; set; }
        public bool RequireLowercase { get; set; }
        public bool RequireUppercase { get; set; }
        public bool RequireDigit { get; set; }
    }
}

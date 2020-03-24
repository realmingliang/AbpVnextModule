using System;
using System.Collections.Generic;
using System.Text;

namespace Tudou.Abp.SettingManagement
{
   public class IdentitySettingsSignInDto
    {
        public bool EnablePhoneNumberConfirmation { get; set; }
        public bool RequireConfirmedEmail { get; set; }
        public bool RequireConfirmedPhoneNumber { get; set; }
    }
}

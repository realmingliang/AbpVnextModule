using System;
using System.Collections.Generic;
using System.Text;

namespace Tudou.Abp.SettingManagement
{
  public  class IdentitySettingsLockOutDto
    {
        public bool AllowedForNewUsers { get; set; }
        public int LockoutDuration { get; set; }
        public int MaxFailedAccessAttempts { get; set; }
    }
}

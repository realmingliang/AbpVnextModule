using System;
using System.Collections.Generic;
using System.Text;

namespace Tudou.Abp.SettingManagement
{
  public  class IdentitySettingsLockOutDto
    {
        public int AllowedForNewUsers { get; set; }
        public int LockoutDuration { get; set; }
        public bool MaxFailedAccessAttempts { get; set; }
    }
}

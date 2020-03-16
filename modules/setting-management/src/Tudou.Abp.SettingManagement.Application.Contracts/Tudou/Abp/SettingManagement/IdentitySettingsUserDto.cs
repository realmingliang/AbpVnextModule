using System;
using System.Collections.Generic;
using System.Text;

namespace Tudou.Abp.SettingManagement
{
  public  class IdentitySettingsUserDto
    {
        public bool IsUserNameUpdateEnabled { get; set; }
        public bool IsEmailUpdateEnabled { get; set; }
    }
}

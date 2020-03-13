using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Tudou.Abp.SettingManagement
{
    public interface IIdentitySettingManagementAppService : IApplicationService
    {
        Task<IdentitySettingsDto> GetAllSettingsAsync();

        Task<IdentitySettingsDto> UpdateAllSettingsAsync(IdentitySettingsDto input);
    }
}

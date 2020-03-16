using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Tudou.Abp.SettingManagement
{
    public interface IAccountSettingsAppService: IApplicationService
    {
        Task<AccountSettingsDto> GetAllSettingsAsync();

        Task UpdateAllSettingsAsync(UpdateAccountSettingsInput input);
    }
}

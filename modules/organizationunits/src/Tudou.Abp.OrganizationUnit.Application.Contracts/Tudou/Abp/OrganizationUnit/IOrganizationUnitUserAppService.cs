using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Tudou.Abp.OrganizationUnit
{
    public interface IOrganizationUnitUserAppService : IApplicationService
    {
        Task<PagedResultDto<OrganizationUnitUserListDto>> GetOrganizationUnitUsersAsync(GetOrganizationUnitUsersInput input);
        Task RemoveUserFromOrganizationUnitAsync(RemoveUserFromOrganizationUnitInput input);
        Task AddUsersToOrganizationUnitAsync(UsersToOrganizationUnitInput input);
        Task<PagedResultDto<NameValueDto>> FindUsersAsync(FindOrganizationUnitUsersInput input);
    }
}

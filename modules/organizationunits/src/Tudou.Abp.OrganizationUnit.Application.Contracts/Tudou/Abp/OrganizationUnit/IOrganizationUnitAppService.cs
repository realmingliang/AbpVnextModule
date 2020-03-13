using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Tudou.Abp.OrganizationUnit
{
    public interface IOrganizationUnitAppService : IApplicationService
    {
        Task<ListResultDto<OrganizationUnitDto>> GetOrganizationUnitsAsync();
        Task<OrganizationUnitDto> CreateOrganizationUnitAsync(CreateOrganizationUnitInput input);
        Task<OrganizationUnitDto> UpdateOrganizationUnitAsync(Guid id,UpdateOrganizationUnitInput input);
        Task<OrganizationUnitDto> MoveOrganizationUnitAsync(Guid id,MoveOrganizationUnitInput input);
        Task DeleteOrganizationUnitAsync(Guid id);
    }
}

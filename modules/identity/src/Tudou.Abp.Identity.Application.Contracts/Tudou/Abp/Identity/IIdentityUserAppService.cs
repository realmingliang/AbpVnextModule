using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Tudou.Abp.Identity
{
    public interface IIdentityUserAppService : ICrudAppService<IdentityUserDto, Guid, GetIdentityUsersInput, IdentityUserCreateDto, IdentityUserUpdateDto>
    {
        Task<ListResultDto<IdentityRoleDto>> GetRolesAsync(Guid id);

        Task UpdateRolesAsync(Guid id, IdentityUserUpdateRolesDto input);

        Task<IdentityUserDto> FindByUsernameAsync(string username);

        Task<IdentityUserDto> FindByEmailAsync(string email);

        Task<List<IdentityUserClaimDto>> GetUserClaimTypes(Guid id);

        Task UpdateUserClaimTypes(Guid id,List<IdentityUserClaimDto> userClaims);


    }
}

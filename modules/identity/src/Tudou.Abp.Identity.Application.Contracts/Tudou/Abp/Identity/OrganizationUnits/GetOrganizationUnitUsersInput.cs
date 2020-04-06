using System;
using Volo.Abp.Application.Dtos;

namespace Tudou.Abp.Identity.OrganizationUnits
{
    public class GetOrganizationUnitUsersInput: PagedResultRequestDto
    {
        public Guid Id { get; set; }

    }
}
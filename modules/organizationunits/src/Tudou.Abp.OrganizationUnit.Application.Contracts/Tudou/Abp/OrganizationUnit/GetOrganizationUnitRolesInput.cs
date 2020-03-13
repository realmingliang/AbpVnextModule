using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Tudou.Abp.OrganizationUnit
{
    public class GetOrganizationUnitRolesInput : PagedResultRequestDto
    {
        public Guid Id { get; set; }
    }
}

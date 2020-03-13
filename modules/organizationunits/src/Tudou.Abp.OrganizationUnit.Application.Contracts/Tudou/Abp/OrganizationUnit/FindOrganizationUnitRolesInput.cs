using System;
using Volo.Abp.Application.Dtos;

namespace Tudou.Abp.OrganizationUnit
{
    public class FindOrganizationUnitRolesInput : PagedResultRequestDto
    {
        public string Filter { get; set; }
        public Guid OrganizationUnitId { get; set; }
    }
}
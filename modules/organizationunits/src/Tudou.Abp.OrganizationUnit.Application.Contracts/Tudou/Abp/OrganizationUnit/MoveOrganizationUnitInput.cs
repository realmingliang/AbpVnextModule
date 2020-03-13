using System;
using Volo.Abp.Application.Dtos;

namespace Tudou.Abp.OrganizationUnit
{
    public class MoveOrganizationUnitInput
    {
        public Guid? NewParentId { get; set; }
    }
}
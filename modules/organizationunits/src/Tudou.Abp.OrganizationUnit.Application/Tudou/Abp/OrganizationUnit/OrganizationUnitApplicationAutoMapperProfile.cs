using AutoMapper;
using Volo.Abp.Identity;

namespace Tudou.Abp.OrganizationUnit
{
    public class OrganizationUnitApplicationAutoMapperProfile : Profile
    {
        public OrganizationUnitApplicationAutoMapperProfile()
        {
            /* You can configure your AutoMapper mapping configuration here.
             * Alternatively, you can split your mapping configurations
             * into multiple profile classes for a better organization. */

            CreateMap<OrganizationUnit, OrganizationUnitDto>()
                 .ForMember(t=>t.MemberCount,t=>t.Ignore())
                 .ForMember(t=> t.RoleCount, t => t.Ignore());
            CreateMap<IdentityRole, OrganizationUnitRoleListDto>()
                 .ForMember(t => t.AddedTime, t => t.Ignore());
            CreateMap<IdentityUser, OrganizationUnitUserListDto>()
                 .ForMember(t => t.AddedTime, t => t.Ignore());
        }
    }
}
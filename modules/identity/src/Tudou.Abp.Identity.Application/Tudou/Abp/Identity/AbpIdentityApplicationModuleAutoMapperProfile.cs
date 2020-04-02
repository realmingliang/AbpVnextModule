using AutoMapper;
using Tudou.Abp.Identity.OrganizationUnits;

namespace Tudou.Abp.Identity
{
    public class AbpIdentityApplicationModuleAutoMapperProfile : Profile
    {
        public AbpIdentityApplicationModuleAutoMapperProfile()
        {
            CreateMap<IdentityUser, IdentityUserDto>();
            CreateMap<IdentityRole, IdentityRoleDto>();
            CreateMap<IdentityUser, ProfileDto>();


            CreateMap<IdentityClaimType, IdentityClaimTypeDto>()
                .ForMember(t => t.ValueTypeAsString, option => option.MapFrom(t => t.ValueType.ToString()));

            CreateMap<IdentityClaimTypeUpdateDto, IdentityClaimType>()
                .ForMember(t => t.IsStatic, option => option.Ignore())
                .ForMember(t => t.ExtraProperties, option => option.Ignore())
                .ForMember(t => t.ConcurrencyStamp, option => option.Ignore())
                .ForMember(t => t.Id, option => option.Ignore());

            CreateMap<IdentityUserClaimDto, IdentityUserClaim>()
                 .ForMember(t => t.TenantId, option => option.Ignore())
                 .ForMember(t => t.Id, option => option.Ignore());
            CreateMap<IdentityUserClaim, IdentityUserClaimDto>();


            CreateMap<OrganizationUnits.OrganizationUnit, OrganizationUnitDto>()
                  .ForMember(t =>t.MemberCount, option => option.Ignore())
                 .ForMember(t => t.RoleCount, option => option.Ignore()); ;
        }
    }
}
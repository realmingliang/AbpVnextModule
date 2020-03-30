using AutoMapper;

namespace Tudou.Abp.Saas
{
    public class AbpSaasApplicationAutoMapperProfile : Profile
    {
        public AbpSaasApplicationAutoMapperProfile()
        {
            CreateMap<SaasTenant, SaasTenantDto>()
                .ForMember(t => t.EditionName, option => option.MapFrom(x => x.SaasEdition.DisplayName));
            CreateMap<SaasEdition, SaasEditionDto>();

        }
    }
}
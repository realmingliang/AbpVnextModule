using AutoMapper;
using Volo.Abp.Data;
using Volo.Abp.MultiTenancy;

namespace Tudou.Abp.Saas
{
    public class AbpSaasDomainMappingProfile : Profile
    {
        public AbpSaasDomainMappingProfile()
        {
            CreateMap<SaasTenant, TenantConfiguration>()
                .ForMember(ti => ti.ConnectionStrings, opts =>
                {
                    opts.MapFrom((tenant, ti) =>
                    {
                        var connStrings = new ConnectionStrings();

                        foreach (var connectionString in tenant.ConnectionStrings)
                        {
                            connStrings[connectionString.Name] = connectionString.Value;
                        }

                        return connStrings;
                    });
                });

            CreateMap<SaasTenant, SaasTenantEto>();
        }
    }
}

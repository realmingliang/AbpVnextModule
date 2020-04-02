using Tudou.Abp.Testing;

namespace Tudou.Abp.Account
{
    public class AbpAccountApplicationTestBase : AbpIntegratedTest<AbpAccountApplicationTestModule>
    {
        protected override void SetAbpApplicationCreationOptions(AbpApplicationCreationOptions options)
        {
            options.UseAutofac();
        }
    }
}
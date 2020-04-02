using System;
using Tudou.Abp.IdentityServer.ApiResources;
using Tudou.Abp.IdentityServer.Clients;
using Tudou.Abp.IdentityServer.Devices;
using Tudou.Abp.IdentityServer.Grants;
using Tudou.Abp.IdentityServer.IdentityResources;
using Volo.Abp;
using Volo.Abp.MongoDB;

namespace Tudou.Abp.IdentityServer.MongoDB
{
    public static class AbpIdentityServerMongoDbContextExtensions
    {
        public static void ConfigureIdentityServer(
            this IMongoModelBuilder builder,
            Action<IdentityServerMongoModelBuilderConfigurationOptions> optionsAction = null)
        {
            Check.NotNull(builder, nameof(builder));

            var options = new IdentityServerMongoModelBuilderConfigurationOptions(
                AbpIdentityServerDbProperties.DbTablePrefix
            );

            optionsAction?.Invoke(options);

            builder.Entity<ApiResource>(b =>
            {
                b.CollectionName = options.CollectionPrefix + "ApiResources";
            });

            builder.Entity<Client>(b =>
            {
                b.CollectionName = options.CollectionPrefix + "Clients";
            });
            builder.Entity<IdentityResource>(b =>
            {
                b.CollectionName = options.CollectionPrefix + "IdentityResources";
            });

            builder.Entity<PersistedGrant>(b =>
            {
                b.CollectionName = options.CollectionPrefix + "PersistedGrants";
            });

            builder.Entity<DeviceFlowCodes>(b =>
            {
                b.CollectionName = options.CollectionPrefix + "DeviceFlowCodes";
            });
        }
    }
}

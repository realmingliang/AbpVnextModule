using System.Linq;
using Microsoft.EntityFrameworkCore;
using Tudou.Abp.IdentityServer.ApiResources;
using Tudou.Abp.IdentityServer.Clients;
using Tudou.Abp.IdentityServer.IdentityResources;

namespace Tudou.Abp.IdentityServer
{
    public static class AbpIdentityServerEfCoreQueryableExtensions
    {
        public static IQueryable<ApiResource> IncludeDetails(this IQueryable<ApiResource> queryable, bool include = true)
        {
            if (!include)
            {
                return queryable;
            }

            return queryable
                .Include(x => x.Secrets)
                .Include(x => x.UserClaims)
                .Include(x => x.Scopes)
                .ThenInclude(s => s.UserClaims);
        }

        public static IQueryable<IdentityResource> IncludeDetails(this IQueryable<IdentityResource> queryable, bool include = true)
        {
            if (!include)
            {
                return queryable;
            }

            return queryable
                .Include(x => x.UserClaims);
        }

        public static IQueryable<Client> IncludeDetails(this IQueryable<Client> queryable, bool include = true)
        {
            if (!include)
            {
                return queryable;
            }

            return queryable
                .Include(x => x.AllowedGrantTypes)
                .Include(x => x.RedirectUris)
                .Include(x => x.PostLogoutRedirectUris)
                .Include(x => x.AllowedScopes)
                .Include(x => x.ClientSecrets)
                .Include(x => x.Claims)
                .Include(x => x.IdentityProviderRestrictions)
                .Include(x => x.AllowedCorsOrigins)
                .Include(x => x.Properties);
        }
    }
}
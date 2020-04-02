using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared.Toolbars;

namespace Tudou.Abp.Account.Web
{
    public class AccountModuleToolbarContributor : IToolbarContributor
    {
        public Task ConfigureToolbarAsync(IToolbarConfigurationContext context)
        {
            if (context.Toolbar.Name != StandardToolbars.Main)
            {
                return Task.CompletedTask;
            }

            //TODO: Currently disabled!
            //if (!context.ServiceProvider.GetRequiredService<ICurrentUser>().IsAuthenticated)
            //{
            //    context.Toolbar.Items.Add(new ToolbarItem(typeof(UserLoginLinkViewComponent)));
            //}

            return Task.CompletedTask;
        }
    }
}

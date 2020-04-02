using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.AspNetCore.Mvc.Razor.Internal;
using Tudou.Abp.Account.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace Tudou.Abp.Account.Web.Pages.Account
{
    public abstract class AccountPage : AbpPage
    {
        [RazorInject]
        public IHtmlLocalizer<AccountResource> L { get; set; }
    }
}

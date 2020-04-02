using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc;
using Tudou.Abp.Identity;
using Volo.Abp;

namespace Tudou.Abp.Account
{
    [RemoteService]
    [Area("account")]
    [Route("api/account")]
    public class AccountController : AbpController, IAccountAppService
    {
        private readonly IAccountAppService _accountAppService;

        public AccountController(IAccountAppService accountAppService)
        {
            _accountAppService = accountAppService;
        }

        [HttpPost]
        [Route("register")]
        public Task<IdentityUserDto> RegisterAsync(RegisterDto input)
        {
            return _accountAppService.RegisterAsync(input);
        }
    }
}
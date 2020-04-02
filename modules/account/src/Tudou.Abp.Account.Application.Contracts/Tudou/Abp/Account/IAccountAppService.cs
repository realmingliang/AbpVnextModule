using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Tudou.Abp.Identity;

namespace Tudou.Abp.Account
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IdentityUserDto> RegisterAsync(RegisterDto input);
    }
}
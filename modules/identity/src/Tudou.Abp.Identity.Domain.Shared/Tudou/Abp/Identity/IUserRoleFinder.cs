using System;
using System.Threading.Tasks;

namespace Tudou.Abp.Identity
{
    public interface IUserRoleFinder
    {
        Task<string[]> GetRolesAsync(Guid userId);
    }
}

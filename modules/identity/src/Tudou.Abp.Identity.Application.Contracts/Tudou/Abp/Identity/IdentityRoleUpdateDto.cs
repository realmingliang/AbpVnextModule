using Volo.Abp.Domain.Entities;

namespace Tudou.Abp.Identity
{
    public class IdentityRoleUpdateDto : IdentityRoleCreateOrUpdateDtoBase, IHasConcurrencyStamp
    {
        public string ConcurrencyStamp { get; set; }
    }
}
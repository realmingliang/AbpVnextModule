using Volo.Abp.Application.Dtos;

namespace Tudou.Abp.Identity
{
    public class GetIdentityUsersInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}

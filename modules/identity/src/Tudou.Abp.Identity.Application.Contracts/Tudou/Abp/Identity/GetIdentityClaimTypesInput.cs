using Volo.Abp.Application.Dtos;

namespace Tudou.Abp.Identity
{
    public class GetIdentityClaimTypesInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}
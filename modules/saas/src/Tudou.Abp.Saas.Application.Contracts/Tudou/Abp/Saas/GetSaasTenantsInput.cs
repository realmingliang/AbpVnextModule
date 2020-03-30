using Volo.Abp.Application.Dtos;

namespace Tudou.Abp.Saas
{
    public class GetSaasTenantsInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}
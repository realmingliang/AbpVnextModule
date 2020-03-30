using Volo.Abp.Application.Dtos;

namespace Tudou.Abp.Saas
{
    public class GetSaasEditionsInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}
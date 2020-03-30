using System.ComponentModel.DataAnnotations;

namespace Tudou.Abp.Saas
{
    public class SaasEditionCreateDto
    {
        [Required]
        [StringLength(SaasEditionConsts.MaxNameLength)]
        public string DisplayName { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;

namespace Tudou.Abp.Saas
{
    public class SaasEditionUpdateDto
    {
        [Required]
        [StringLength(SaasEditionConsts.MaxNameLength)]
        public string DisplayName { get; set; }
    }
}
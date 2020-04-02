using Tudou.Abp.Account.Web.Pages.Account;
using Tudou.Abp.Identity;
using AutoMapper;

namespace Tudou.Abp.Account.Web
{
    public class AbpAccountWebAutoMapperProfile : Profile
    {
        public AbpAccountWebAutoMapperProfile()
        {
            CreateMap<ProfileDto, PersonalSettingsInfoModel>();
        }
    }
}

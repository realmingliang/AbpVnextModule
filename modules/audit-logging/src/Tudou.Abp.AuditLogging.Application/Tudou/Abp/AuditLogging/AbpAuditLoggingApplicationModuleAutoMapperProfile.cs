using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tudou.Abp.AuditLogging
{
   public class AbpAuditLoggingApplicationModuleAutoMapperProfile: Profile
    {
        public AbpAuditLoggingApplicationModuleAutoMapperProfile()
        {
            CreateMap<AuditLog, AuditLogDto>()
                .ForMember(t => t.EntityChanges, option => option.MapFrom(l => l.EntityChanges))
                .ForMember(t => t.Actions, option => option.MapFrom(l => l.Actions));
            CreateMap<EntityChange, EntityChangeDto>()
                 .ForMember(t => t.PropertyChanges, option => option.MapFrom(l => l.PropertyChanges));
            CreateMap<AuditLogAction, AuditLogActionDto>();
            CreateMap<EntityPropertyChange, EntityPropertyChangeDto>();
        }
    }
}

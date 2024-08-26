using AutoMapper;
using RuiChen.AbpPro.AuditLogging;
using RuiChen.AbpPro.Logging;

namespace RuiChen.AbpPro.Auditing
{
    public class AbpProAuditingMapperProfile : Profile
    {
        public AbpProAuditingMapperProfile()
        {
            CreateMap<AuditLogAction, AuditLogActionDto>()
                .MapExtraProperties();
            CreateMap<EntityPropertyChange, EntityPropertyChangeDto>();
            CreateMap<EntityChangeWithUsername, EntityChangeWithUsernameDto>();
            CreateMap<EntityChange, EntityChangeDto>()
                .MapExtraProperties();
            CreateMap<AuditLog, AuditLogDto>()
                .MapExtraProperties();

            CreateMap<SecurityLog, SecurityLogDto>()
                .MapExtraProperties();

            CreateMap<LogField, LogFieldDto>();
            CreateMap<LogException, LogExceptionDto>();
            CreateMap<LogInfo, LogDto>();
        }
    }
}

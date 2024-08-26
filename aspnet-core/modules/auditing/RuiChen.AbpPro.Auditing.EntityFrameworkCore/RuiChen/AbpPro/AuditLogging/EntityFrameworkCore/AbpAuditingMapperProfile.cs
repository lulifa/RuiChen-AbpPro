using AutoMapper;

namespace RuiChen.AbpPro.AuditLogging
{
    public class AbpAuditingMapperProfile : Profile
    {
        public AbpAuditingMapperProfile()
        {
            CreateMap<Volo.Abp.AuditLogging.AuditLogAction, AuditLogAction>()
                .MapExtraProperties();

            CreateMap<Volo.Abp.AuditLogging.EntityPropertyChange, EntityPropertyChange>();

            CreateMap<Volo.Abp.AuditLogging.EntityChange, EntityChange>()
                .MapExtraProperties();

            CreateMap<Volo.Abp.AuditLogging.AuditLog, AuditLog>()
                .MapExtraProperties();

            CreateMap<Volo.Abp.Identity.IdentitySecurityLog, SecurityLog>()
                .MapExtraProperties();
        }
    }
}

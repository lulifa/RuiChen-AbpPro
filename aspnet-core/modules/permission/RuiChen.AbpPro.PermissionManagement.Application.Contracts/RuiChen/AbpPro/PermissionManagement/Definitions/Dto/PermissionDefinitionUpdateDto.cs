using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities;

namespace RuiChen.AbpPro.PermissionManagement
{
    public class PermissionDefinitionUpdateDto : PermissionDefinitionCreateOrUpdateDto, IHasConcurrencyStamp
    {
        [StringLength(40)]
        public string ConcurrencyStamp { get; set; }
    }
}

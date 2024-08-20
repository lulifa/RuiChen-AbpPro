using System.ComponentModel.DataAnnotations;
using Volo.Abp.PermissionManagement;
using Volo.Abp.Validation;

namespace RuiChen.AbpPro.PermissionManagement
{
    public class PermissionGroupDefinitionCreateDto : PermissionGroupDefinitionCreateOrUpdateDto
    {
        [Required]
        [DynamicStringLength(typeof(PermissionGroupDefinitionRecordConsts), nameof(PermissionGroupDefinitionRecordConsts.MaxNameLength))]
        public string Name { get; set; }
    }
}

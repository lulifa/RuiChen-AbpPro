using Volo.Abp.Application.Dtos;
using Volo.Abp.Validation;

namespace RuiChen.AbpPro.Platform
{
    public class MenuGetAllInput : ISortedResultRequest
    {
        [DynamicStringLength(typeof(LayoutConsts), nameof(LayoutConsts.MaxFrameworkLength))]
        public string Framework { get; set; }

        public string Filter { get; set; }

        public bool Reverse { get; set; }

        public Guid? ParentId { get; set; }

        public string Sorting { get; set; }

        public Guid? LayoutId { get; set; }
    }
}

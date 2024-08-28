using Volo.Abp.Application.Dtos;

namespace RuiChen.AbpPro.Platform
{
    public class DataDto : EntityDto<Guid>
    {
        public string Name { get; set; }

        public string Code { get; set; }

        public string DisplayName { get; set; }

        public string Description { get; set; }

        public Guid? ParentId { get; set; }

        public List<DataItemDto> Items { get; set; } = new List<DataItemDto>();
    }
}

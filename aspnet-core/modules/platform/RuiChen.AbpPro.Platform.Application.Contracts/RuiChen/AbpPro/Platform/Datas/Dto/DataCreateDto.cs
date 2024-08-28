namespace RuiChen.AbpPro.Platform
{
    public class DataCreateDto : DataCreateOrUpdateDto
    {
        public Guid? ParentId { get; set; }
    }
}

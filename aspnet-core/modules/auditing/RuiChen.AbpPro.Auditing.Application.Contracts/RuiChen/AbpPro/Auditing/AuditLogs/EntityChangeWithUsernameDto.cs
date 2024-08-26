namespace RuiChen.AbpPro.Auditing
{
    public class EntityChangeWithUsernameDto
    {
        public EntityChangeDto EntityChange { get; set; }

        public string UserName { get; set; }
    }
}

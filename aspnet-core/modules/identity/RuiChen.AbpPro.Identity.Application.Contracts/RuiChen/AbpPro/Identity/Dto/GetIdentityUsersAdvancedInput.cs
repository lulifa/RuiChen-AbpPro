using Volo.Abp.Identity;

namespace RuiChen.AbpPro.Identity
{
    public class GetIdentityUsersAdvancedInput : GetIdentityUsersInput
    {
        public bool IncludeDetails { get; set; }

        public Guid? RoleId { get; set; }

        public Guid? OrganizationUnitId { get; set; }

        public string UserName { get; set; }

        public string PhoneNumber { get; set; }

        public string EmailAddress { get; set; }

        public string Name { get; set; }

        public string SurName { get; set; }

        public bool? IsLockedOut { get; set; }

        public bool? NotActive { get; set; }

        public bool? EmailConfirmed { get; set; }

        public bool? IsExternal { get; set; }

        public DateTime? MaxCreationTime { get; set; }

        public DateTime? MinCreationTime { get; set; }

        public DateTime? MaxModifitionTime { get; set; }

        public DateTime? MinModificationTime { get; set; }

    }
}

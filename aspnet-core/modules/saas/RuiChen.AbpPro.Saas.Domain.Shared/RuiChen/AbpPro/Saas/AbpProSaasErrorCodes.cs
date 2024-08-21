namespace RuiChen.AbpPro.Saas
{
    public static class AbpProSaasErrorCodes
    {
        public const string Namespace = "Saas";

        public const string DuplicateEditionDisplayName = Namespace + ":010001";

        public const string DeleteUsedEdition = Namespace + ":010002";

        public const string DuplicateTenantName = Namespace + ":020001";

        public const string NamespaceMultiTenancy = "Volo.AbpIo.MultiTenancy";

        public const string TenantRestricted = NamespaceMultiTenancy + ":010001";

        public const string TenantUnavailable= NamespaceMultiTenancy + ":010002";

    }
}

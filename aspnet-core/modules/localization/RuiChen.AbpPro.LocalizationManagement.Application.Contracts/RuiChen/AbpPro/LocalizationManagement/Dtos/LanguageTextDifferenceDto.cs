namespace RuiChen.AbpPro.LocalizationManagement
{
    public class LanguageTextDifferenceDto
    {
        public string CultureName { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public string ResourceName { get; set; }
        public string TargetCultureName { get; set; }
        public string TargetValue { get; set; }

        public int CompareTo(LanguageTextDifferenceDto other)
        {
            return other.ResourceName.CompareTo(ResourceName) ^ other.Key.CompareTo(Key);
        }
    }
}

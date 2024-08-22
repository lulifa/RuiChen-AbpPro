namespace RuiChen.AbpPro.Localization
{
    public class AbpProLocalizationCultureMapOptions
    {

        public List<CultureMapInfo> CulturesMaps { get; }

        public List<CultureMapInfo> UiCulturesMaps { get; }

        public AbpProLocalizationCultureMapOptions()
        {
            CulturesMaps = new List<CultureMapInfo>();
            UiCulturesMaps = new List<CultureMapInfo>();
        }

    }
}

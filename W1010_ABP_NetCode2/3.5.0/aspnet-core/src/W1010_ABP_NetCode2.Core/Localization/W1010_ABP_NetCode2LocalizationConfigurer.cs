using Abp.Configuration.Startup;
using Abp.Localization.Dictionaries;
using Abp.Localization.Dictionaries.Xml;
using Abp.Reflection.Extensions;

namespace W1010_ABP_NetCode2.Localization
{
    public static class W1010_ABP_NetCode2LocalizationConfigurer
    {
        public static void Configure(ILocalizationConfiguration localizationConfiguration)
        {
            localizationConfiguration.Sources.Add(
                new DictionaryBasedLocalizationSource(W1010_ABP_NetCode2Consts.LocalizationSourceName,
                    new XmlEmbeddedFileLocalizationDictionaryProvider(
                        typeof(W1010_ABP_NetCode2LocalizationConfigurer).GetAssembly(),
                        "W1010_ABP_NetCode2.Localization.SourceFiles"
                    )
                )
            );
        }
    }
}

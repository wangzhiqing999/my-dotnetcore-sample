using System.Reflection;
using Abp.Configuration.Startup;
using Abp.Localization.Dictionaries;
using Abp.Localization.Dictionaries.Xml;
using Abp.Reflection.Extensions;

namespace W1001_ABP_With_Zero.Localization
{
    public static class W1001_ABP_With_ZeroLocalizationConfigurer
    {
        public static void Configure(ILocalizationConfiguration localizationConfiguration)
        {
            localizationConfiguration.Sources.Add(
                new DictionaryBasedLocalizationSource(W1001_ABP_With_ZeroConsts.LocalizationSourceName,
                    new XmlEmbeddedFileLocalizationDictionaryProvider(
                        typeof(W1001_ABP_With_ZeroLocalizationConfigurer).GetAssembly(),
                        "W1001_ABP_With_Zero.Localization.SourceFiles"
                    )
                )
            );
        }
    }
}
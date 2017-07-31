using System.Collections.Generic;
using Abp.Localization;
using Microsoft.AspNetCore.Http;

namespace W1000_ABP_HelloWorld.Web.Views.Shared.Components.LanguageSelection
{
    public class LanguageSelectionViewModel
    {
        public LanguageInfo CurrentLanguage { get; set; }

        public IReadOnlyList<LanguageInfo> Languages { get; set; }

        public PathString CurrentUrl { get; set; }
    }
}
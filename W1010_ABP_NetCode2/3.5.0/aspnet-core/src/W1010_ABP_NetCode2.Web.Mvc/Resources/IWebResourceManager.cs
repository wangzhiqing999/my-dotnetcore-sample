using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Razor;

namespace W1010_ABP_NetCode2.Web.Resources
{
    public interface IWebResourceManager
    {
        void AddScript(string url, bool addMinifiedOnProd = true);

        IReadOnlyList<string> GetScripts();

        HelperResult RenderScripts();
    }
}

using Abp.Application.Services;

namespace W1000_ABP_HelloWorld
{
    /// <summary>
    /// Derive your application services from this class.
    /// </summary>
    public abstract class W1000_ABP_HelloWorldAppServiceBase : ApplicationService
    {
        protected W1000_ABP_HelloWorldAppServiceBase()
        {
            LocalizationSourceName = W1000_ABP_HelloWorldConsts.LocalizationSourceName;
        }
    }
}
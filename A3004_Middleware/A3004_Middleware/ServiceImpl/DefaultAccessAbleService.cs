using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using A3004_Middleware.Service;


namespace A3004_Middleware.ServiceImpl
{
    public class DefaultAccessAbleService : IAccessAbleService
    {
        bool IAccessAbleService.IsAccessAble(string path)
        {
            if(string.Equals(path, "/Home/TestAccessDisable", StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            return true;
        }
    }
}

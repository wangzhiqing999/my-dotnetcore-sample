using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace A3004_Middleware.Service
{
    public interface IAccessAbleService
    {

        /// <summary>
        /// 是否可访问.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        bool IsAccessAble(string path);

    }
}

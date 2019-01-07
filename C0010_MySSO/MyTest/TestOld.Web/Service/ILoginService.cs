using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Refit;
using TestOld.Web.ServiceModel;

namespace TestOld.Web.Service
{

    public interface ILoginService
    {

        /// <summary>
        /// 查询 Token 是否已登录.
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        [Get("/Account/IsLogin")]
        Task<CommonServiceResult<LoginResultData>> IsLogin(Guid token);

    }
}

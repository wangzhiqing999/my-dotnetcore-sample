using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using P0002_MyEtf.Model;
using P0002_MyEtf.ServiceModel;

namespace P0002_MyEtf.Service
{

    /// <summary>
    /// ETF周线数据服务.
    /// </summary>
    public interface IEtfWeekService
    {

        /// <summary>
        /// 计算 ETF周线数据.
        /// </summary>
        /// <param name="etfCode"></param>
        /// <param name="tradingDate"></param>
        /// <returns></returns>
        ServiceResult CalculateEtfWeekLine(string etfCode, DateTime tradingDate);


    }
}

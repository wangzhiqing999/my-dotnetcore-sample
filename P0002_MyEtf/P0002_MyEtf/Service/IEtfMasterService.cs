using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using P0002_MyEtf.Model;


namespace P0002_MyEtf.Service
{

    /// <summary>
    /// ETF 主数据服务.
    /// </summary>
    public interface IEtfMasterService
    {

        /// <summary>
        /// 获取 Etf.
        /// </summary>
        /// <param name="etfCode"></param>
        /// <returns></returns>
        EtfMaster GetEtfMaster(string etfCode);


        /// <summary>
        /// 获取 Etf 列表.
        /// </summary>
        /// <returns></returns>
        List<EtfMaster> GetEtfMasterList();


    }
}

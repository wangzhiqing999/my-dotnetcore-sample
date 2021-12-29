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
    /// ETF日线数据服务.
    /// </summary>
    public interface IEtfDayService
    {


        /// <summary>
        /// 插入 ETF日线数据.
        /// </summary>
        /// <param name="etfDayLine"></param>
        /// <returns></returns>
        ServiceResult InsertEtfDayLine(EtfDayLine etfDayLine);


        /// <summary>
        /// 获取 ETF 日线数据.
        /// </summary>
        /// <param name="etfCode"></param>
        /// <returns></returns>
        List<EtfDayLine> GetEtfDayLines(string etfCode);



        /// <summary>
        /// 计算 ETF日波幅数据.
        /// </summary>
        /// <param name="etfCode"></param>
        /// <param name="tradingDate"></param>
        /// <returns></returns>
        ServiceResult CalculateEtfDayTr(string etfCode, DateTime tradingDate);



        /// <summary>
        /// 计算 ETF 日 EMA.
        /// </summary>
        /// <param name="etfCode"></param>
        /// <param name="tradingDate"></param>
        /// <returns></returns>
        ServiceResult CalculateEtfDayEma(string etfCode, DateTime tradingDate);



        /// <summary>
        /// 计算 ETF日 MACD.
        /// </summary>
        /// <param name="etfCode"></param>
        /// <param name="tradingDate"></param>
        /// <returns></returns>
        ServiceResult CalculateEtfDayMacd(string etfCode, DateTime tradingDate);





        /// <summary>
        /// 获取日线的 MA.
        /// <br/>
        /// 这个是通过调用 Postgres 下面写的函数来实现的.
        /// </summary>
        /// <param name="etfCode"></param>
        /// <param name="maNum"></param>
        /// <returns></returns>
        List<EtfMaData> GetEtfDayMa(string etfCode, int maNum);


    }
}

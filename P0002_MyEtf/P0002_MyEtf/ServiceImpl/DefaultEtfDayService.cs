using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using P0002_MyEtf.DataAccess;
using P0002_MyEtf.Model;
using P0002_MyEtf.Service;
using P0002_MyEtf.ServiceModel;



namespace P0002_MyEtf.ServiceImpl
{
    public class DefaultEtfDayService : IEtfDayService
    {


        private readonly MyEtfContext _MyEtfContext;


        private readonly ILogger<DefaultEtfDayService> _Logger;



        public DefaultEtfDayService(MyEtfContext context, ILogger<DefaultEtfDayService> logger)
        {
            this._MyEtfContext = context;
            this._Logger = logger;
        }



        /// <summary>
        /// 插入 ETF日线数据.
        /// </summary>
        /// <param name="etfDayLine"></param>
        /// <returns></returns>
        public ServiceResult InsertEtfDayLine(EtfDayLine etfDayLine)
        {
            if (this._Logger.IsEnabled(LogLevel.Debug))
            {
                this._Logger.LogDebug($"InsertEtfDayLine Start. etfCode = {etfDayLine.EtfCode}, tradingDate = {etfDayLine.TradingDate:yyyy-MM-dd}");
            }

            try
            {
                var dbData = this._MyEtfContext.EtfDayLines.Find(etfDayLine.EtfCode, etfDayLine.TradingDate);
                if(dbData != null)
                {
                    return ServiceResult.DataHadExistsResult;
                }

                this._MyEtfContext.EtfDayLines.Add(etfDayLine);
                this._MyEtfContext.SaveChanges();
                return ServiceResult.DefaultSuccessResult;
            }
            catch (Exception ex)
            {
                this._Logger.LogError(ex, ex.Message);
                return new ServiceResult(ex);
            }
        }





        /// <summary>
        /// 计算 ATR 的参数.
        /// </summary>
        private const int ATR_PARAM = 14;



        /// <summary>
        /// 计算 ETF日波幅数据.
        /// </summary>
        /// <param name="EtfCode"></param>
        /// <param name="tradingDate"></param>
        /// <returns></returns>
        public ServiceResult CalculateEtfDayTr(string etfCode, DateTime tradingDate)
        {
            if (this._Logger.IsEnabled(LogLevel.Debug))
            {
                this._Logger.LogDebug($"CalculateEtfDayTr Start. etfCode = {etfCode}, tradingDate = {tradingDate:yyyy-MM-dd}");
            }

            try
            {
                EtfDayLine etfDayLine = this._MyEtfContext.EtfDayLines.Find(etfCode, tradingDate);
                if (etfDayLine == null)
                {
                    return ServiceResult.DataNotFoundResult;
                }

                // 查询前一天的交易数据.
                var prevQuery =
                    from data in _MyEtfContext.EtfDayLines
                    where
                        data.EtfCode == etfDayLine.EtfCode
                        && data.TradingDate < etfDayLine.TradingDate
                    orderby data.TradingDate descending
                    select data;

                EtfDayLine prevEtfDayLine = prevQuery.FirstOrDefault();

                // TR=∣最高价-最低价∣和∣最高价-昨收∣和∣昨收-最低价∣的最大值
                List<decimal> tempDataList = new List<decimal>();
                // 最高价-最低价
                tempDataList.Add(Math.Abs(etfDayLine.HighestPrice - etfDayLine.LowestPrice));

                if (prevEtfDayLine != null)
                {
                    // 最高价-昨收
                    tempDataList.Add(Math.Abs(etfDayLine.HighestPrice - prevEtfDayLine.ClosePrice));
                    // 昨收-最低价
                    tempDataList.Add(Math.Abs(prevEtfDayLine.ClosePrice - etfDayLine.LowestPrice));
                }

                decimal tr = tempDataList.Max();


                // 查询前 N 天的交易数据.
                var query =
                    from data in _MyEtfContext.EtfDayTrs
                    where
                        data.EtfCode == etfDayLine.EtfCode
                        && data.TradingDate < etfDayLine.TradingDate
                    orderby data.TradingDate descending
                    select data.Tr;


                // 用于平均的行数（目的： 避免前 N 天， 日期不足， 平均的时候，分母过大.）
                int rowCount = 1;

                // 前 n-1 天.
                decimal trSummary = 0;
                foreach (decimal oneTr in query.Take(ATR_PARAM - 1))
                {
                    trSummary += oneTr;
                    rowCount++;
                }

                // 加 今天.
                trSummary += tr;

                // 平均.
                decimal atr = trSummary / rowCount;


                EtfDayTr etfDayTr = this._MyEtfContext.EtfDayTrs.Find(etfCode, tradingDate);
                if(etfDayTr == null)
                {
                    // 插入.
                    etfDayTr = new EtfDayTr();
                    etfDayTr.EtfCode = etfCode;
                    etfDayTr.TradingDate = tradingDate;
                    etfDayTr.Tr = tr;
                    etfDayTr.Atr = atr;

                    this._MyEtfContext.EtfDayTrs.Add(etfDayTr);
                } 
                else
                {
                    // 简单更新.
                    etfDayTr.Tr = tr;
                    etfDayTr.Atr = atr;
                }




                this._MyEtfContext.SaveChanges();
                return ServiceResult.DefaultSuccessResult;
            }
            catch (Exception ex)
            {
                this._Logger.LogError(ex, ex.Message);
                return new ServiceResult(ex);
            }
        }


    }
}

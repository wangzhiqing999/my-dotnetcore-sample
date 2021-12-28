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
    public partial class DefaultEtfDayService : IEtfDayService
    {


        private const int MACD_FAST_PARAM = 12;

        private const int MACD_SLOW_PARAM = 26;

        private const int MACD_DIF_PARAM = 9;




        /// <summary>
        /// 计算 ETF日 MACD.
        /// </summary>
        /// <param name="etfCode"></param>
        /// <param name="tradingDate"></param>
        /// <returns></returns>
        public ServiceResult CalculateEtfDayMacd(string etfCode, DateTime tradingDate)
        {
            if (this._Logger.IsEnabled(LogLevel.Debug))
            {
                this._Logger.LogDebug($"CalculateEtfDayMacd Start. etfCode = {etfCode}, tradingDate = {tradingDate:yyyy-MM-dd}");
            }


            try
            {
                EtfDayLine etfDayLine = this._MyEtfContext.EtfDayLines.Find(etfCode, tradingDate);
                if (etfDayLine == null)
                {
                    return ServiceResult.DataNotFoundResult;
                }


                // 计算今日 MACD 时， 需要前一日的 DEA 数据.
                var prevQuery =
                    from data in this._MyEtfContext.EtfDayMacds
                    where
                        data.EtfCode == etfCode
                        && data.TradingDate < tradingDate
                    orderby
                        data.TradingDate descending
                    select
                        data;

                EtfDayMacd prevData = prevQuery.FirstOrDefault();

                if(prevData == null)
                {
                    // 没有小于今天的数据.
                    // 意味着， 需要从零开始计算.
                    InitCalculateMacd(etfCode);
                    
                } 
                else
                {
                    // 存在前一天的数据.
                    // 只计算当天.
                    CalculateMacdOneDay(etfCode, tradingDate, prevData);
                }


                return ServiceResult.DefaultSuccessResult;
            }
            catch (Exception ex)
            {
                this._Logger.LogError(ex, ex.Message);
                return new ServiceResult(ex);
            }
        }



        /// <summary>
        /// 初始化计算 MACD.
        /// </summary>
        /// <param name="etfCode"></param>
        private void InitCalculateMacd(string etfCode)
        {
            var query =
                from data in this._MyEtfContext.EtfDayEmas
                where
                    data.EtfCode == etfCode
                orderby
                    data.TradingDate ascending
                select
                    data;

            List<EtfDayEma> emaList = query.ToList();


            List<MacdCalculateValue> dataList = new List<MacdCalculateValue>();            
            for (int i = 0; i < emaList.Count; i++)
            {
                MacdCalculateValue data = new MacdCalculateValue()
                {
                    TradingDate = emaList[i].TradingDate,
                };
                data.EmaFast = emaList[i].Ema12;
                data.EmaSlow = emaList[i].Ema26;

                dataList.Add(data);
            }




            List<EtfDayMacd> macdList = new List<EtfDayMacd>();
            for(int i = 0; i < dataList.Count; i ++)
            {
                MacdCalculateValue data = dataList[i];
                EtfDayMacd macd = new EtfDayMacd()
                {
                    EtfCode = etfCode,
                    TradingDate = data.TradingDate,
                    // DIFF = EMA(12) - EMA(26)
                    Diff = data.EmaFast - data.EmaSlow,
                };

                // DEA = 2/(9+1) * 今日DIFF + 8/(9+1) * 昨日DEA
                if(i > 0)
                {
                    macd.Dea = macd.Diff * 2 / (MACD_DIF_PARAM + 1) + macdList[i - 1].Dea * (MACD_DIF_PARAM - 1) / (MACD_DIF_PARAM + 1);
                } 
                else
                {
                    macd.Dea = macd.Diff;
                }
                macd.Macd = 2 * (macd.Diff - macd.Dea);
                macdList.Add(macd);
            }

            this._MyEtfContext.EtfDayMacds.AddRange(macdList);
            this._MyEtfContext.SaveChanges();
        }



        private void CalculateMacdOneDay(string etfCode, DateTime tradingDate, EtfDayMacd prevMacd)
        {
            var query =
                from data in this._MyEtfContext.EtfDayEmas
                where
                    data.EtfCode == etfCode
                    && data.TradingDate == tradingDate
                select
                    data;

            EtfDayEma emaData = query.FirstOrDefault();


            EtfDayMacd todayMacd = this._MyEtfContext.EtfDayMacds.Find(etfCode, tradingDate);
            if (todayMacd == null)
            {
                todayMacd = new EtfDayMacd()
                {
                    EtfCode = etfCode,
                    TradingDate = tradingDate,
                };
                // DIFF = EMA(12) - EMA(26)
                todayMacd.Diff = emaData.Ema12 - emaData.Ema26;
                todayMacd.Dea = todayMacd.Diff * 2 / (MACD_DIF_PARAM + 1) + prevMacd.Dea * (MACD_DIF_PARAM - 1) / (MACD_DIF_PARAM + 1);
                todayMacd.Macd = 2 * (todayMacd.Diff - todayMacd.Dea);

                this._MyEtfContext.EtfDayMacds.Add(todayMacd);
            } 
            else
            {
                todayMacd.Diff = emaData.Ema12 - emaData.Ema26;
                todayMacd.Dea = todayMacd.Diff * 2 / (MACD_DIF_PARAM + 1) + prevMacd.Dea * (MACD_DIF_PARAM - 1) / (MACD_DIF_PARAM + 1);
                todayMacd.Macd = 2 * (todayMacd.Diff - todayMacd.Dea);
            }

            this._MyEtfContext.SaveChanges();
        }








    }
}

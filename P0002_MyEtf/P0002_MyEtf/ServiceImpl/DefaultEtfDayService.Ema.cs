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

        /// <summary>
        /// 计算 ETF 日 EMA.
        /// </summary>
        /// <param name="etfCode"></param>
        /// <param name="tradingDate"></param>
        /// <returns></returns>
        public ServiceResult CalculateEtfDayEma(string etfCode, DateTime tradingDate)
        {
            if (this._Logger.IsEnabled(LogLevel.Debug))
            {
                this._Logger.LogDebug($"CalculateEtfDayEma Start. etfCode = {etfCode}, tradingDate = {tradingDate:yyyy-MM-dd}");
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
                    from data in this._MyEtfContext.EtfDayEmas
                    where
                        data.EtfCode == etfCode
                        && data.TradingDate < tradingDate
                    orderby
                        data.TradingDate descending
                    select
                        data;

                EtfDayEma prevData = prevQuery.FirstOrDefault();

                if (prevData == null)
                {
                    // 没有小于今天的数据.
                    // 意味着， 需要从零开始计算.
                    InitCalculateEma(etfCode);
                } 
                else
                {

                    EtfDayEma todayEtfDayEma = this._MyEtfContext.EtfDayEmas.Find(etfCode, tradingDate);
                    if(todayEtfDayEma == null)
                    {
                        todayEtfDayEma = new EtfDayEma()
                        {
                            EtfCode = etfCode,
                            TradingDate = tradingDate,

                            Ema12 = etfDayLine.ClosePrice * 2 / 13 + prevData.Ema12 * 11 / 13,
                            Ema26 = etfDayLine.ClosePrice * 2 / 27 + prevData.Ema12 * 25 / 27
                        };

                        this._MyEtfContext.EtfDayEmas.Add(todayEtfDayEma);
                    } 
                    else
                    {
                        todayEtfDayEma.Ema12 = etfDayLine.ClosePrice * 2 / 13 + prevData.Ema12 * 11 / 13;
                        todayEtfDayEma.Ema26 = etfDayLine.ClosePrice * 2 / 27 + prevData.Ema12 * 25 / 27;
                    }
                    
                    this._MyEtfContext.SaveChanges();
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
        /// 初始化计算 EMA.
        /// </summary>
        /// <param name="etfCode"></param>
        private void InitCalculateEma(string etfCode)
        {
            var query =
                from data in this._MyEtfContext.EtfDayLines
                where
                    data.EtfCode == etfCode
                orderby
                    data.TradingDate ascending
                select
                    data;

            List<EtfDayLine> kLineList = query.ToList();

            // 所有的收盘数据.
            List<decimal> allDatas = kLineList.Select(p => p.ClosePrice).ToList();


            List<EmaCalculateValue> data12List = new List<EmaCalculateValue>();
            List<EmaCalculateValue> data26List = new List<EmaCalculateValue>();

            for (int i = 0; i < kLineList.Count; i++)
            {
                EmaCalculateValue data12 = new EmaCalculateValue()
                {
                    TradingDate = kLineList[i].TradingDate,
                };
                data12.EmaValue = GetEma(allDatas, data12List, i, 12);
                data12List.Add(data12);


                EmaCalculateValue data26 = new EmaCalculateValue()
                {
                    TradingDate = kLineList[i].TradingDate,
                };
                data26.EmaValue = GetEma(allDatas, data26List, i, 26);
                data26List.Add(data26);
            }


            for(int i =0; i < kLineList.Count; i++)
            {

                EtfDayEma dayEma = new EtfDayEma()
                {
                    EtfCode = etfCode,
                    TradingDate = kLineList[i].TradingDate,
                    Ema12 = data12List[i].EmaValue,
                    Ema26 = data26List[i].EmaValue
                };

                this._MyEtfContext.EtfDayEmas.Add(dayEma);
            }

            this._MyEtfContext.SaveChanges();
        }

        private decimal GetEma(List<decimal> allDatas, List<EmaCalculateValue> emaDatas, int myIndex, int emaParam)
        {
            if (myIndex == 0)
            {
                // 向前递归到第一天了.
                return allDatas[0];
            }

            decimal myVal = allDatas[myIndex] * 2 / (emaParam + 1);
            decimal otherVal = emaDatas[myIndex - 1].EmaValue * (emaParam - 1) / (emaParam + 1);

            return myVal + otherVal;
        }


    }
}

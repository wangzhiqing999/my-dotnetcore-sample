using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using P0002_MyEtf.Model;
using P0002_MyEtf.Service;
using P0002_MyEtf.ServiceModel;


namespace P0002_MyEtf.ServiceImpl
{

    /// <summary>
    /// MACD 周线 交易策略.
    /// </summary>
    public class WeekMacdTradingStrategyService : ITradingStrategyService
    {

        private readonly ILogger<WeekMacdTradingStrategyService> _Logger;


        private readonly IEtfWeekService _EtfWeekService;


        public WeekMacdTradingStrategyService(IEtfWeekService etfWeekService, ILogger<WeekMacdTradingStrategyService> logger)
        {
            this._EtfWeekService = etfWeekService;
            this._Logger = logger;
        }



        /// <summary>
        /// MACD 周线数据.
        /// </summary>
        private List<EtfWeekMacd> _MacdDataList;


        public TradingSignal GetTradingSignal(string etfCode, DateTime tradingDate)
        {

            if(_MacdDataList == null || _MacdDataList.Count == 0)
            {
                // 周次加载.
                _MacdDataList = this._EtfWeekService.GetEtfWeekMacd(etfCode);
            }
            else
            {
                if (_MacdDataList.Count > 0 && _MacdDataList[0].EtfCode != etfCode)
                {
                    // 调用 一个 ETF 以后， 又调用另外一个 ETF.
                    _MacdDataList = this._EtfWeekService.GetEtfWeekMacd(etfCode);
                }
            }
            // 上面这一段的处理，主要是用于处理两种情况.
            // 1. 针对一个 ETF， 从上市日开始，一直计算到今天.
            //    那么，预期，this._EtfWeekService.GetEtfWeekMacd(etfCode) 就只执行一次.
            //
            // 2. 在周末，针对所有的 ETF, 都执行一遍 当天的情况.
            //   那么，预期，this._EtfWeekService.GetEtfWeekMacd(etfCode) 需要执行多次.(次数 = ETF 数量)

            if (_MacdDataList.Count == 0)
            {
                // 无数据.
                this._Logger.LogWarning($"GetEtfWeekMacd {etfCode}. Data not found!");
                return TradingSignal.None;
            }


            // 获取位置.
            int dataIndex = _MacdDataList.FindIndex(p => p.TradingDate == tradingDate);

            if(dataIndex == -1)
            {
                // 数据不存在.
                this._Logger.LogWarning($"FindIndex {tradingDate:yyyy-MM-dd}. Data not found!");
                return TradingSignal.None;
            }

            if(dataIndex < 10)
            {
                // 忽略前10行数据.
                return TradingSignal.None;
            }

            
            // 上周数据.
            EtfWeekMacd prevData = _MacdDataList[dataIndex - 1];
            // 本周数据.
            EtfWeekMacd thisData = _MacdDataList[dataIndex];

            if(prevData.Macd == 0)
            {
                // 上周是零. 向前推.
                do
                {
                    dataIndex--;
                    if (dataIndex < 10)
                    {
                        // 忽略前10行数据.
                        return TradingSignal.None;
                    }
                    prevData = _MacdDataList[dataIndex - 1];
                } while (prevData.Macd == 0);
            } 
            

            if(thisData.Macd == 0)
            {
                // 本周 MACD = 0.
                // 可以认为是 交叉了.
                if(prevData.Macd > 0)
                {
                    // 由  大于0 变为0.
                    return TradingSignal.Sell;
                } 
                else
                {
                    // 由  小于0 变为0.

                    return TradingSignal.Buy;
                }
            }


            if (thisData.Macd > 0)
            {
                // 本周 MACD > 0.
                if(prevData.Macd < 0)
                {
                    // 由  小于0 变为大于0.
                    return TradingSignal.Buy;
                }
            }

            if(thisData.Macd < 0)
            {
                // 本周 MACD < 0.
                if (prevData.Macd > 0)
                {
                    // 由  大于0 变为小于0.
                    return TradingSignal.Sell;
                }
            }


            // 其它情况，认为是 “没有信号”！
            return TradingSignal.None;
        }
    }
}

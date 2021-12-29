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
    /// 双日均线 交易策略.
    /// </summary>
    public class TwoDayMaTradingStrategyService : ITradingStrategyService
    {

        private readonly ILogger<TwoDayMaTradingStrategyService> _Logger;


        private readonly IEtfDayService _EtfDayService;


        public TwoDayMaTradingStrategyService(IEtfDayService etfWeekService, ILogger<TwoDayMaTradingStrategyService> logger)
        {
            this._EtfDayService = etfWeekService;
            this._Logger = logger;
        }




        /// <summary>
        /// 短周期均线数.
        /// </summary>
        public int ShortMaNum { set; get; } = 20;


        /// <summary>
        /// 长周期均线数.
        /// </summary>
        public int LongMaNum { set; get; } = 60;




        /// <summary>
        /// 短周期 MA 数据.
        /// </summary>
        private List<EtfMaData> _ShortMaDataList;

        
        /// <summary>
        /// 长周期 MA 数据.
        /// </summary>
        private List<EtfMaData> _LongMaDataList;




        public TradingSignal GetTradingSignal(string etfCode, DateTime tradingDate)
        {

            if (_ShortMaDataList == null || _ShortMaDataList.Count == 0)
            {
                // 周次加载.
                _ShortMaDataList = this._EtfDayService.GetEtfDayMa(etfCode, ShortMaNum);
                _LongMaDataList = this._EtfDayService.GetEtfDayMa(etfCode, LongMaNum);
            }
            else
            {
                if (_ShortMaDataList.Count > 0 && _ShortMaDataList[0].EtfCode != etfCode)
                {
                    // 调用 一个 ETF 以后， 又调用另外一个 ETF.
                    _ShortMaDataList = this._EtfDayService.GetEtfDayMa(etfCode, ShortMaNum);
                    _LongMaDataList = this._EtfDayService.GetEtfDayMa(etfCode, LongMaNum);
                }
            }





            if (_ShortMaDataList.Count == 0)
            {
                // 无数据.
                this._Logger.LogWarning($"GetEtfDayMa {etfCode}, {ShortMaNum}. Data not found!");
                return TradingSignal.None;
            }            
            if (_LongMaDataList.Count == 0)
            {
                // 无数据.
                this._Logger.LogWarning($"GetEtfDayMa {etfCode}, {LongMaNum}. Data not found!");
                return TradingSignal.None;
            }


            // 获取位置.
            int dataIndex = _ShortMaDataList.FindIndex(p => p.TradingDate == tradingDate);


            if (dataIndex == -1)
            {
                // 数据不存在.
                this._Logger.LogWarning($"FindIndex {tradingDate:yyyy-MM-dd}. Data not found!");
                return TradingSignal.None;
            }


            if (dataIndex < LongMaNum)
            {
                // 忽略前 LongMaNum 行数据.
                return TradingSignal.None;
            }



            EtfMaData prevShortData = _ShortMaDataList[dataIndex - 1];
            EtfMaData prevLongData = _LongMaDataList[dataIndex - 1];

            EtfMaData thisShortData = _ShortMaDataList[dataIndex];
            EtfMaData thisLongData = _LongMaDataList[dataIndex];


            this._Logger.LogDebug($"{tradingDate:yyyy-MM-dd} --- MA({ShortMaNum})={thisShortData.MaValue}; MA({LongMaNum})={thisLongData.MaValue}");

            if(prevShortData.MaValue == prevLongData.MaValue)
            {
                // 前一天是相同的.
                do
                {
                    dataIndex--;
                    if (dataIndex < LongMaNum)
                    {
                        // 忽略前 长周期 行数据.
                        return TradingSignal.None;
                    }
                    prevShortData = _ShortMaDataList[dataIndex - 1];
                    prevLongData = _LongMaDataList[dataIndex - 1];
                } while (prevShortData.MaValue == prevLongData.MaValue);                                    
            }
            

            if (thisShortData.MaValue == thisLongData.MaValue)
            {
                // 今天的 MA短 = MA 长
                // 可以认为是 交叉了.
                if(prevShortData.MaValue > prevLongData.MaValue)
                {
                    // 由 短周期均线在长周期均线上， 变为交叉.
                    return TradingSignal.Sell;
                }
                else
                {
                    // 由 短周期均线在长周期均线下， 变为交叉.
                    return TradingSignal.Buy;
                }
            }



            if (thisShortData.MaValue > thisLongData.MaValue)
            {
                if (prevShortData.MaValue < prevLongData.MaValue)
                {
                    // 由 短周期均线在长周期均线下， 变为交叉： 短期均线在 长期均线上.
                    return TradingSignal.Buy;
                }
            }


            if (thisShortData.MaValue < thisLongData.MaValue)
            {
                if (prevShortData.MaValue > prevLongData.MaValue)
                {
                    // 由 短周期均线在长周期均线上， 变为交叉： 短期均线在 长期均线下.
                    return TradingSignal.Sell;
                }
            }


            // 其它情况，认为是 “没有信号”！
            return TradingSignal.None;
        }


    }
}

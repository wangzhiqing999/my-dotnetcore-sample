﻿using System;
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
    public partial class DefaultEtfWeekService : IEtfWeekService
    {


        private readonly MyEtfContext _MyEtfContext;


        private readonly ILogger<DefaultEtfWeekService> _Logger;



        public DefaultEtfWeekService(MyEtfContext context, ILogger<DefaultEtfWeekService> logger)
        {
            this._MyEtfContext = context;
            this._Logger = logger;
        }




        public ServiceResult CalculateEtfWeekLine(string etfCode, DateTime tradingDate)
        {

            if (this._Logger.IsEnabled(LogLevel.Debug))
            {
                this._Logger.LogDebug($"CalculateEtfWeekLine Start. etfCode = {etfCode}, tradingDate = {tradingDate:yyyy-MM-dd}");
            }

            try
            {

                if(tradingDate.DayOfWeek == DayOfWeek.Saturday || tradingDate.DayOfWeek == DayOfWeek.Sunday)
                {
                    return new ServiceResult("NOT_TRADING_DAY", "周六周日非交易日！");
                }
               
                // 1 表示星期一
                int dayIndex = Convert.ToInt32(tradingDate.DayOfWeek);
                // 计算出本周的周一那一天.
                DateTime startDate = tradingDate.AddDays(1 - dayIndex);


                var query =
                    from data in _MyEtfContext.EtfDayLines
                    where
                        data.EtfCode == etfCode
                        && data.TradingDate >= startDate
                        && data.TradingDate <= tradingDate
                    orderby data.TradingDate
                    select data;

                List<EtfDayLine> etfDayLineList = query.ToList();


                if(etfDayLineList.Count == 0)
                {
                    // 数据不存在.
                    return ServiceResult.DataNotFoundResult;
                }



                // 主键是 代码 与 最后交易日.
                // 只能先删除，后插入.
                var removeQuery =
                    from data in _MyEtfContext.EtfWeekLines
                    where
                        data.EtfCode == etfCode
                        && data.TradingDate >= startDate
                        && data.TradingDate <= tradingDate
                    select data;
                List<EtfWeekLine> removeDataList = removeQuery.ToList();

                foreach(var removeItem in removeDataList)
                {
                    _MyEtfContext.EtfWeekLines.Remove(removeItem);
                }


                // 新增.
                EtfWeekLine etfWeekLine = new EtfWeekLine()
                {
                    EtfCode = etfCode,
                    TradingDate = etfDayLineList.Last().TradingDate
                };

                // 开盘.
                etfWeekLine.OpenPrice = etfDayLineList.First().OpenPrice;
                // 收盘.
                etfWeekLine.ClosePrice = etfDayLineList.Last().ClosePrice;
                // 最高.
                etfWeekLine.HighestPrice = etfDayLineList.Select(p => p.HighestPrice).Max();
                // 最低.
                etfWeekLine.LowestPrice = etfDayLineList.Select(p => p.LowestPrice).Min();
                // 成交.
                etfWeekLine.Volume = etfDayLineList.Select(p => p.Volume).Sum();

                this._MyEtfContext.EtfWeekLines.Add(etfWeekLine);
                




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
        /// 获取 ETF 周线数据.
        /// </summary>
        /// <param name="etfCode"></param>
        /// <returns></returns>
        public List<EtfWeekLine> GetEtfWeekLines(string etfCode)
        {
            var query =
                from data in this._MyEtfContext.EtfWeekLines
                where
                    data.EtfCode == etfCode
                orderby
                    data.TradingDate
                select
                    data;

            List<EtfWeekLine> resultList = query.ToList();

            return resultList;
        }




        /// <summary>
        /// 获取最后一条 ETF 周线数据.
        /// </summary>
        /// <param name="etfCode"></param>
        /// <returns></returns>
        public EtfWeekLine GetLastEtfWeekLines(string etfCode)
        {
            var query =
                from data in this._MyEtfContext.EtfWeekLines
                where
                    data.EtfCode == etfCode
                orderby
                    data.TradingDate descending
                select
                    data;

            EtfWeekLine result = query.FirstOrDefault();

            return result;
        }



    }
}

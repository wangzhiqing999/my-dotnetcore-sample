using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;

using MyFramework.ServiceImpl;
using MyFramework.ServiceModel;
using MyFramework.Util;

using MyWork.Model;
using MyWork.DataAccess;
using MyWork.Service;

using MyWork.ServiceModel;


namespace MyWork.ServiceImpl
{


    public class DefaultDailySummaryServiceImpl : IDailySummaryService
    {

        /// <summary>
        /// 数据服务.
        /// </summary>
        private MyWorkContext context;


        /// <summary>
        /// 价格读取.
        /// </summary>
        private IPriceReader priceReader;



        public DefaultDailySummaryServiceImpl(MyWorkContext context, IPriceReader priceReader)
        {
            this.context = context;
            this.priceReader = priceReader;
        }






        /// <summary>
        /// 创建每日总结.
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        CommonServiceResult IDailySummaryService.BuildDailySummary(DateTime date)
        {
            try
            {
                // 先查询当前持仓.
                var query =
                    from data in context.Positions.Include("StockInfoData")
                    where
                        // 状态有效.
                        data.Status == Position.STATUS_IS_ACTIVE
                        // 有持仓.
                        && data.Quantity != 0
                    select
                        data;

                // 持仓列表.
                List<Position> positionList = query.ToList();


                // 准备更新的每日总结数据.
                List<DailySummary> newDataList = new List<DailySummary>();


                // 遍历每一个持仓.
                foreach (Position userPosition in positionList)
                {
                    // 获取收盘价.
                    decimal closePrice = this.priceReader.GetClosePrice(userPosition.StockInfoData.QueryCode);

                    DailySummary newData = new DailySummary()
                    {
                        // 日期.
                        DailySummaryDate = date,

                        // 账户.
                        AccountID = userPosition.AccountID,

                        // 股票.
                        StockCode = userPosition.StockCode,

                        // 持仓数量.
                        PositionQuantity = userPosition.Quantity,

                        // 收盘.
                        ClosePrice = closePrice,

                        // 市值.
                        PositionValue = userPosition.Quantity * closePrice,

                    };


                    // 获取前日数据.
                    DailySummary prevData =
                            context.DailySummarys.Where(p => 
                                p.AccountID == newData.AccountID && p.StockCode == newData.StockCode
                                && p.DailySummaryDate < newData.DailySummaryDate).OrderByDescending(p => p.DailySummaryDate).FirstOrDefault();

                    if (prevData != null)
                    {
                        // 存在前日数据.
                        newData.StopLossPrice = prevData.StopLossPrice;
                    }

                    if (newData.ClosePrice <= newData.StopLossPrice)
                    {
                        // 当日收盘低于止损.
                        // 明日应当完成平仓操作.
                        newData.Todo = "准备平仓";
                    }

                    newDataList.Add(newData);
                }


                // 先删除历史数据（如果有.）
                var oldQuery =
                    from data in context.DailySummarys
                    where
                        data.DailySummaryDate == date
                    select data;

                List<DailySummary> oldDataList = oldQuery.ToList();
                if (oldDataList.Count > 0)
                {
                    context.DailySummarys.RemoveRange(oldDataList);
                }


                // 插入新数据.
                context.DailySummarys.AddRange(newDataList);


                // 保存.
                context.SaveChanges();


                // 返回.
                return CommonServiceResult.DefaultSuccessResult;
            }
            catch (Exception ex)
            {
                return new CommonServiceResult(ex);
            }
        }



        /// <summary>
        /// 生成账户的日结报表
        /// </summary>
        /// <param name="reportDate"></param>
        /// <returns></returns>
        CommonServiceResult IDailySummaryService.BuildDailyReport(DateTime reportDate)
        {
            // 报表结束时间.
            DateTime reportFinishTime = reportDate.AddDays(1);

            try
            {
                // 查询所有账户.
                var query =
                    from data in context.Accounts
                    where
                        // 状态有效.
                        data.Status == Account.STATUS_IS_ACTIVE
                    select data;

                List<Account> accountList = query.ToList();


                // 遍历每一个账户.
                foreach(Account account in accountList)
                {
                    // 检查是否已经存在有 指定账户，当日的报表数据.
                    var oldReportData = context.AccountDailyReports.SingleOrDefault(p => p.AccountID == account.AccountID && p.ReportDate == reportDate);
                    if (oldReportData != null)
                    {
                        // 数据已存在， 需要先删除.
                        context.AccountDailyReports.Remove(oldReportData);
                    }

                    // 新的报表数据.
                    AccountDailyReport newReport = new AccountDailyReport()
                    {
                        // 账户.
                        AccountID = account.AccountID,
                        // 日期.
                        ReportDate = reportDate,
                    };

                    // 查询是否存在 前日报表.
                    var reportQuery =
                        from data in context.AccountDailyReports
                        where
                            data.AccountID == account.AccountID
                            && data.ReportDate < reportDate
                        orderby
                            data.ReportDate descending
                        select
                            data;

                    var lastDailyData = reportQuery.FirstOrDefault();

                    if (lastDailyData == null)
                    {
                        // 不存在 历史报表.                            
                        // 期初资金.
                        newReport.BeginningMoney = 0;
                    }
                    else
                    {
                        // 存在 历史报表.
                        // 需要判断.
                        if (lastDailyData.ReportDate >= reportDate)
                        {
                            // 最后一个报表的日期， 大于等于 计算的日期。
                            // 报错.
                            CommonServiceResult errResult = new CommonServiceResult()
                            {
                                ResultCode = "EXISTS_NEWER_REPORT_DATA",
                                ResultMessage = String.Format("已经存在有 {0:yyyy-MM-dd} 的报表数据， 不能计算之前的报表数据！", lastDailyData.ReportDate)
                            };
                            return errResult;
                        }
                        // 这一期的 期初  = 上期的 期末.
                        newReport.BeginningMoney = lastDailyData.EndingMoney;
                    }


                    // 计算当日交易.
                    var logQuery =
                        from data in context.AccountOperationLogs
                        where
                            data.AccountID == account.AccountID
                            && data.AccountingDate >= reportDate
                            && data.AccountingDate < reportFinishTime
                        orderby
                            data.OperationTime
                        select
                            data;


                    List<AccountOperationLog> logList = logQuery.ToList();

                    // 交易笔数.
                    newReport.DealCount = logList.Count();
                    // 变化金额.
                    newReport.MoneyChange = logList.Sum(p => p.OperationMoney);


                    // 期末金额.
                    if (newReport.DealCount == 0)
                    {
                        // 无交易.
                        // 期末 = 期初.
                        newReport.EndingMoney = newReport.BeginningMoney;
                    }
                    else
                    {
                        // 期末 = 日志中， 最后一行的 处理后.
                        newReport.EndingMoney = logList.Last().AfterAccountBalance;
                    }



                    // 查询当日总结.
                    DailySummary dailySummary = context.DailySummarys.FirstOrDefault(p => p.AccountID == account.AccountID && p.DailySummaryDate == reportDate);

                    if(dailySummary != null)
                    {
                        // 持仓市值.
                        newReport.PositionValue = dailySummary.PositionValue;
                    }

                    // 插入数据.
                    context.AccountDailyReports.Add(newReport);
                    // 保存.
                    context.SaveChanges();

                }

                // 返回.
                return CommonServiceResult.DefaultSuccessResult;
            }
            catch (Exception ex)
            {
                return new CommonServiceResult(ex);
            }


        }

    }
}

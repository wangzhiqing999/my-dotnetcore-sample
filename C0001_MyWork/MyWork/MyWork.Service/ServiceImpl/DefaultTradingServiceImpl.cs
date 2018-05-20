using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

using MyFramework.ServiceImpl;
using MyFramework.ServiceModel;
using MyFramework.Util;

using MyWork.Model;
using MyWork.DataAccess;
using MyWork.Service;

using MyWork.ServiceModel;


namespace MyWork.ServiceImpl
{

    /// <summary>
    ///  交易 服务.
    /// </summary>
    public class DefaultTradingServiceImpl : ITradingService
    {

        /// <summary>
        /// 数据服务.
        /// </summary>
        private MyWorkContext context;


        public DefaultTradingServiceImpl(MyWorkContext context)
        {
            this.context = context;
        }




        /// <summary>
        /// 新增交易
        /// </summary>
        /// <param name="tradingData"></param>
        /// <returns></returns>
        public CommonServiceResult NewTrading(Trading tradingData)
        {
            try
            {
                // ########## 基本数据检查. ##########

                // 查询账户.
                Account account = context.Accounts.Find(tradingData.AccountID);
                if (account == null)
                {
                    // 账户不存在.
                    return WorkServiceResult.AccountIdNotFoundResult;
                }

                // 检查账户余额
                if(account.AccountBalance < tradingData.Cost)
                {
                    // 余额不足.
                    return WorkServiceResult.AccountBalanceErrorResult;
                }


                // 查询股票.
                StockInfo stockInfo = context.StockInfos.Find(tradingData.StockCode);
                if (stockInfo == null)
                {
                    // 股票代码不存在.
                    return WorkServiceResult.StockCodeNotFoundResult;
                }


                // 首先， 插入交易.
                context.Tradings.Add(tradingData);

                // 然后. 插入或更新持仓.
                Position positionData = context.Positions.SingleOrDefault(
                    p => p.StockCode == tradingData.StockCode 
                        && p.AccountID == tradingData.AccountID 
                        && p.Status == Position.STATUS_IS_ACTIVE);

                if (positionData == null)
                {
                    // 新的持仓.
                    positionData = new Position()
                    {
                        // 股票代码.
                        StockCode = tradingData.StockCode,
                        // 账户.
                        AccountID = tradingData.AccountID,

                        // 数量
                        Quantity = tradingData.Quantity,

                        // 成本.
                        Cost = tradingData.Cost,

                        // 状态.
                        IsActive = true,
                    };
                    positionData.BeforeInsertOperation("AUTO");
                    context.Positions.Add(positionData);
                }
                else
                {
                    // 更新持仓.
                    // 数量
                    positionData.Quantity = positionData.Quantity + tradingData.Quantity;
                    // 成本.
                    positionData.Cost = positionData.Cost + tradingData.Cost;
                }


                // 然后，更新账户金额.

                // 变更后的余额 = 当前余额 - 本次交易的成本.
                decimal afterAccountBalance = account.AccountBalance - tradingData.Cost;

                // 先创建账户日志.
                AccountOperationLog logData = new AccountOperationLog()
                {
                    // 关联账户ID.
                    AccountID = tradingData.AccountID,

                    // 结算日期.
                    AccountingDate = tradingData.TradingDateTime,

                    // 操作时间.
                    OperationTime = DateTime.Now,
                    // 操作金额.
                    OperationMoney = tradingData.Cost,
                    //// 操作备注.
                    //OperationDesc = desc,

                    // 业务处理前 账户金额.
                    BeforeAccountBalance = account.AccountBalance,
                    // 业务处理后 账户金额.
                    AfterAccountBalance = afterAccountBalance,
                };

                // 插入日志.
                context.AccountOperationLogs.Add(logData);

                // 更新账户金额 = 业务处理后 账户金额.
                account.AccountBalance = afterAccountBalance;

                // 物理保存.
                context.SaveChanges();

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

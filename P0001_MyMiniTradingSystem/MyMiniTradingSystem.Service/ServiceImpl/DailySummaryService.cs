using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



using log4net;


using MyMiniTradingSystem.Model;
using MyMiniTradingSystem.DataAccess;
using MyMiniTradingSystem.Service;




namespace MyMiniTradingSystem.ServiceImpl
{


    public class DailySummaryService
    {

        /// <summary>
        /// 日志处理类.
        /// </summary>
        protected static ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);



        /// <summary>
        /// 结果消息.
        /// </summary>
        public string ResultMessage { set; get; }





        /// <summary>
        /// 创建某用户的每日总结.
        /// </summary>
        /// <param name="userCode"></param>
        /// <returns></returns>
        public bool BuildOneUserDailySummary(string userCode, DateTime date)
        {
            bool result = false;

            try
            {

                using (MyMiniTradingSystemContext context = new MyMiniTradingSystemContext())
                {

                    // 先查询有没有仓位.
                    var pQuery =
                        from data in context.Positions
                        where
                            data.UserCode == userCode
                            && data.Quantity > 0
                        select
                            data;


                    List<Position> userPositionList = pQuery.ToList();

                    if (userPositionList.Count == 0)
                    {
                        ResultMessage = "用户没有任何持仓！";
                        return result;
                    }



                    List<DailySummary> newDataList = new List<DailySummary>();


                    foreach (Position userPosition in userPositionList)
                    {

                        CommodityPrice cp =
                            context.CommodityPrices.FirstOrDefault(p => p.CommodityCode == userPosition.CommodityCode && p.TradingStartDate == date);

                        if (cp == null)
                        {
                            ResultMessage = String.Format("未能检索到{0}的{1:yyyy-MM-dd}的行情数据！",userPosition.CommodityCode, date);
                            return result;
                        }


                        DailySummary newData = new DailySummary()
                        {
                            // 用户.
                            UserCode = userCode,

                            // 商品.
                            PositionCommodityCode = userPosition.CommodityCode,

                            // 日期.
                            DailySummaryDate = date,

                            // 数量.
                            PositionQuantity = userPosition.Quantity,

                            // 收盘.
                            ClosePrice = cp.ClosePrice,

                            // 市值.
                            PositionValue = userPosition.Quantity * cp.ClosePrice,
                        };


                        


                        // 止损 = 收盘 - 3ATR.
                        newData.StopLossPrice = newData.ClosePrice - 3 * cp.Atr;


                        // 取得前日数据.
                        DailySummary prevData =
                            context.DailySummarys.Where(p => p.UserCode == newData.UserCode
                                && p.PositionCommodityCode == newData.PositionCommodityCode
                                && p.DailySummaryDate < newData.DailySummaryDate).OrderByDescending(p => p.DailySummaryDate).FirstOrDefault();

                        if (prevData != null)
                        {
                            // 存在前日数据.
                            if (prevData.StopLossPrice > newData.StopLossPrice)
                            {
                                // 浮动止损， 只能上升，不能下降.
                                newData.StopLossPrice = prevData.StopLossPrice;
                            }
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
                            data.UserCode == userCode
                            && data.DailySummaryDate == date
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

                    result = true;
                }

            }
            catch (Exception ex)
            {
                logger.Error(ex.Message, ex);

                result = false;
                ResultMessage = ex.Message;
            }




            return result;


        }





        /// <summary>
        /// 更新每日总结.
        /// </summary>
        /// <param name="newData"></param>
        /// <returns></returns>
        public bool InsertOrUpdateDailySummary(DailySummary newData)
        {
            bool result = false;

            try
            {

                using (MyMiniTradingSystemContext context = new MyMiniTradingSystemContext())
                {
                    // 查询数据是否已存在.
                    var query =
                        from data in context.DailySummarys
                        where
                            data.PositionCommodityCode == newData.PositionCommodityCode
                            && data.UserCode == newData.UserCode
                            && data.DailySummaryDate == newData.DailySummaryDate
                        select data;


                    DailySummary oldData = query.FirstOrDefault();

                    if (oldData != null)
                    {
                        // 更新.

                        // 数量.
                        oldData.PositionQuantity = newData.PositionQuantity;
                        // 收.
                        oldData.ClosePrice = newData.ClosePrice;
                        // 市值.
                        oldData.PositionValue = newData.PositionValue;
                        // 止损.
                        oldData.StopLossPrice = newData.StopLossPrice;

                    }
                    else
                    {
                        // 插入.
                        context.DailySummarys.Add(newData);
                    }

                    // 保存.
                    context.SaveChanges();

                    result = true;
                }

            }
            catch (Exception ex)
            {
                logger.Error(ex.Message, ex);

                result = false;
                ResultMessage = ex.Message;
            }




            return result;
        }


    }


}

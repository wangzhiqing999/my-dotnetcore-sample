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


    public class CommodityPriceService : ICommodityPriceService
    {

        /// <summary>
        /// 日志处理类.
        /// </summary>
        protected static ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);




        /// <summary>
        /// 更新每日行情.
        /// </summary>
        /// <param name="newData"></param>
        /// <returns></returns>
        public ServiceResult InsertOrUpdateCommodityPrice(CommodityPrice newData)
        {
            ServiceResult result;

            try
            {

                using (MyMiniTradingSystemContext context = new MyMiniTradingSystemContext())
                {

                    // 计算 Tr.
                    newData.Tr = GetTrValue(context, newData);

                    // 计算 Atr.
                    newData.Atr = GetAtrValue(context, newData);


                    // 查询数据是否已存在.
                    var query =
                        from data in context.CommodityPrices
                        where 
                            data.CommodityCode == newData.CommodityCode
                            && data.TradingStartDate == newData.TradingStartDate
                        select data;


                    CommodityPrice oldData = query.FirstOrDefault();

                    if (oldData != null)
                    {
                        // 更新.

                        // 开.
                        oldData.OpenPrice = newData.OpenPrice;
                        // 收.
                        oldData.ClosePrice = newData.ClosePrice;
                        // 高.
                        oldData.HighestPrice = newData.HighestPrice;
                        // 低.
                        oldData.LowestPrice = newData.LowestPrice;
                        // 量.
                        oldData.Volume = newData.Volume;

                        // Tr.
                        oldData.Tr = newData.Tr;
                        // ATR.
                        oldData.Atr = newData.Atr;
                    }
                    else
                    {
                        // 插入.
                        context.CommodityPrices.Add(newData);
                    }

                    // 保存.
                    context.SaveChanges();

                    // 成功.
                    result = ServiceResult.SuccessServiceResult;
                }

            }
            catch (Exception ex)
            {
                logger.Error(ex.Message, ex);

                // 失败.
                result = new ServiceResult(-1, ex.Message);
            }

            return result;
        }






        /// <summary>
        /// 计算 TR
        /// </summary>
        /// <param name="context"></param>
        /// <param name="newData"></param>
        /// <returns></returns>
        private static decimal GetTrValue(MyMiniTradingSystemContext context, CommodityPrice newData)
        {
            // 查询前一天的交易数据.
            var query =
                from data in context.CommodityPrices
                where
                    data.CommodityCode == newData.CommodityCode
                    && data.TradingStartDate < newData.TradingStartDate
                orderby data.TradingStartDate descending
                select data;


            CommodityPrice prevData = query.FirstOrDefault();

            
            // TR=∣最高价-最低价∣和∣最高价-昨收∣和∣昨收-最低价∣的最大值
            List<decimal> tempDataList = new List<decimal>();

            // 最高价-最低价
            tempDataList.Add(Math.Abs(newData.HighestPrice - newData.LowestPrice));

            if (prevData != null)
            {
                // 最高价-昨收
                tempDataList.Add(Math.Abs(newData.HighestPrice - prevData.ClosePrice));
                // 昨收-最低价
                tempDataList.Add(Math.Abs(prevData.ClosePrice - newData.LowestPrice));
            }


            return tempDataList.Max();
        }



        /// <summary>
        /// 计算 ATR.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="newData"></param>
        /// <param name="atrParam"></param>
        /// <returns></returns>
        private static decimal GetAtrValue(MyMiniTradingSystemContext context, CommodityPrice newData, int atrParam = 14)
        {
            // 查询前 N 天的交易数据.
            var query =
                from data in context.CommodityPrices
                where
                    data.CommodityCode == newData.CommodityCode
                    && data.TradingStartDate < newData.TradingStartDate
                orderby data.TradingStartDate descending
                select data.Tr;


            // 用于平均的行数（目的： 避免前 N 天， 日期不足， 平均的时候，分母过大.）
            int rowCount = 1;

            // 前 n-1 天.
            decimal resultData = 0;
            foreach (decimal oneTr in query.Take(atrParam - 1))
            {
                resultData += oneTr;

                rowCount++;
            }

            // 加 今天.
            resultData += newData.Tr;

            // 平均.
            resultData = resultData / rowCount;

            return resultData;
        }


    }

}

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

    public class PositionService : IPositionService
    {
        /// <summary>
        /// 日志处理类.
        /// </summary>
        protected static ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);



        /// <summary>
        /// 创建仓位.
        /// </summary>
        /// <param name="newData"></param>
        /// <returns></returns>
        public ServiceResult OpenPosition(Position newData)
        {
            ServiceResult result;

            try
            {

                using (MyMiniTradingSystemContext context = new MyMiniTradingSystemContext())
                {

                    // 查询数据是否已存在.
                    var query =
                        from data in context.Positions
                        where
                            data.CommodityCode == newData.CommodityCode
                            && data.UserCode == newData.UserCode
                        select data;


                    Position oldData = query.FirstOrDefault();

                    if (oldData != null)
                    {
                        // 更新.
                        oldData.Quantity = oldData.Quantity + newData.Quantity;
                    }
                    else
                    {
                        // 新增.
                        context.Positions.Add(newData);
                    }




                    // 保存.
                    context.SaveChanges();

                    // 执行成功.
                    result = ServiceResult.SuccessServiceResult;
                }

            }
            catch (Exception ex)
            {
                logger.Error(ex.Message, ex);

                result = new ServiceResult(-1, ex.Message);
            }




            return result;
        }




        /// <summary>
        /// 平仓.
        /// </summary>
        /// <param name="newData"></param>
        /// <returns></returns>
        public ServiceResult ClosePosition(Position newData)
        {
            ServiceResult result;

            try
            {

                using (MyMiniTradingSystemContext context = new MyMiniTradingSystemContext())
                {

                    // 查询数据是否已存在.
                    var query =
                        from data in context.Positions
                        where
                            data.CommodityCode == newData.CommodityCode
                            && data.UserCode == newData.UserCode
                        select data;


                    Position oldData = query.FirstOrDefault();

                    if (oldData != null)
                    {
                        // 更新.

                        if (oldData.Quantity < newData.Quantity)
                        {
                            result = new ServiceResult(-1,
                                String.Format("仓位只有{0}，无法平仓{1}。", oldData.Quantity, newData.Quantity));
                            return result;
                        }
                        oldData.Quantity = oldData.Quantity - newData.Quantity;
                    }
                    else
                    {
                        // 数据不存在.
                        result = new ServiceResult(-1, "没有相关的仓位可供平仓");
                        return result;
                    }




                    // 保存.
                    context.SaveChanges();

                    // 执行成功.
                    result = ServiceResult.SuccessServiceResult;
                }

            }
            catch (Exception ex)
            {
                logger.Error(ex.Message, ex);

                result = new ServiceResult(-1, ex.Message);
            }




            return result;
        }






    }

}

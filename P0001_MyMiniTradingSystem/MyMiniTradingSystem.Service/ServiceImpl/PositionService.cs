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

    public class PositionService
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
        /// 创建仓位.
        /// </summary>
        /// <param name="newData"></param>
        /// <returns></returns>
        public bool OpenPosition(Position newData)
        {
            bool result = false;

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
        /// 平仓.
        /// </summary>
        /// <param name="newData"></param>
        /// <returns></returns>
        public bool ClosePosition(Position newData)
        {
            bool result = false;

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
                            ResultMessage = String.Format("仓位只有{0}，无法平仓{1}。", oldData.Quantity, newData.Quantity);
                            return result;
                        }
                        oldData.Quantity = oldData.Quantity - newData.Quantity;
                    }
                    else
                    {
                        // 数据不存在.
                        ResultMessage = "没有相关的仓位可供平仓。";
                        return result;
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

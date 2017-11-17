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


    public class TradableCommodityService : ITradableCommodityService
    {
        /// <summary>
        /// 日志处理类.
        /// </summary>
        protected static ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


        /// <summary>
        /// 插入商品信息
        /// </summary>
        /// <param name="newData"></param>
        /// <returns></returns>
        public ServiceResult CreateTradableCommodity(TradableCommodity newData)
        {
            ServiceResult result;

            try
            {

                using (MyMiniTradingSystemContext context = new MyMiniTradingSystemContext())
                {
                    // 查询数据是否已存在.
                    var query =
                        from data in context.TradableCommoditys
                        where data.CommodityCode == newData.CommodityCode
                        select data;

                    if (query.Count() > 0)
                    {
                        result = new ServiceResult(-1, String.Format("代码为{0}的商品已存在！", newData.CommodityCode));
                        return result;
                    }

                    context.TradableCommoditys.Add(newData);
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
        /// 获取所有的商品信息.
        /// </summary>
        /// <returns></returns>
        public List<TradableCommodity> GetAll()
        {
            using (MyMiniTradingSystemContext context = new MyMiniTradingSystemContext())
            {
                var query =
                    from data in context.TradableCommoditys
                    select data;

                var resultList = query.ToList();

                return resultList;
            }
        }



        public TradableCommodity Get(string code)
        {
            using (MyMiniTradingSystemContext context = new MyMiniTradingSystemContext())
            {
                // 查询数据是否已存在.
                var query =
                    from data in context.TradableCommoditys
                    where data.CommodityCode == code
                    select data;

                var resultList = query.FirstOrDefault();

                return resultList;
            }
        }



    }


}

using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

using MyFramework.ServiceImpl;
using MyFramework.ServiceModel;
using MyFramework.Util;

using MyAuthentication.ServiceModel;

using MyWork.Model;
using MyWork.DataAccess;
using MyWork.Service;

using MyWork.ServiceModel;


namespace MyWork.ServiceImpl
{

    /// <summary>
    /// 股票池服务
    /// </summary>
    public class DefaultStockPoolServiceImpl : DefaultCommonService, IStockPoolService
    {
        /// <summary>
        /// 数据服务.
        /// </summary>
        private MyWorkContext context;


        public DefaultStockPoolServiceImpl(MyWorkContext context)
        {
            this.context = context;
        }




        /// <summary>
        /// 检查股票池名称是否已存在.
        /// </summary>
        /// <param name="organizationID"></param>
        /// <param name="stockPoolName"></param>
        /// <returns></returns>
        private bool IsExistsStockPoolName(long organizationID, string stockPoolName)
        {
            // 股票池名称检查.
            var item = context.StockPools.FirstOrDefault(p => p.OrganizationID == organizationID && p.StockPoolName == stockPoolName);
            if(item != null)
            {
                // 指定的组织下， 存在有指定的名称。
                return true;
            }
            return false;
        }



        /// <summary>
        /// 基本的 组织代码 / 股票池代码 数据检查.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="organizationID"></param>
        /// <param name="stockPoolID"></param>
        /// <returns></returns>
        private CommonServiceResult BasicOrganizationCheck(long organizationID, long stockPoolID)
        {
            // 查询.
            StockPool data = context.StockPools.Find(stockPoolID);

            if (data == null)
            {
                // 数据不存在.
                return WorkServiceResult.StockPoolIDNotFoundResult;
            }
            if (data.OrganizationID != organizationID)
            {
                // 组织ID不匹配.
                return AuthenticationServiceResult.OrganizationNotMatchResult;
            }

            // 返回.
            return CommonServiceResult.DefaultSuccessResult;
        }






        /// <summary>
        /// 创建股票池.
        /// </summary>
        /// <param name="stockPool"></param>
        /// <returns></returns>
        CommonServiceResult IStockPoolService.CreateStockPool(StockPool stockPool)
        {
            try
            {
               
                //using (MyWorkContext context = new MyWorkContext())
                {
                    // 股票池名称检查.
                    if (IsExistsStockPoolName(stockPool.OrganizationID, stockPool.StockPoolName))
                    {
                        // 股票池名称已存在.
                        return WorkServiceResult.StockPoolNameHadExistsResult;
                    }

                    // 检查完毕.
                    // 插入.
                    context.StockPools.Add(stockPool);
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


        /// <summary>
        /// 更新股票池.
        /// </summary>
        /// <param name="stockPool"></param>
        /// <returns></returns>
        CommonServiceResult IStockPoolService.UpdateStockPool(StockPool stockPool)
        {
            try
            {
                //using (MyWorkContext context = new MyWorkContext())
                {
                    // 查询.
                    StockPool data = context.StockPools.Find(stockPool.StockPoolID);

                    if (data == null)
                    {
                        // 数据不存在.
                        return WorkServiceResult.StockPoolIDNotFoundResult;
                    }

                    if (data.OrganizationID != stockPool.OrganizationID)
                    {
                        // 不允许修改组织代码
                        CommonServiceResult errResult = AuthenticationServiceResult.OrganizationIDModifyResult;
                        return errResult;
                    }

                    if(data.StockPoolName != stockPool.StockPoolName)
                    {
                        // 修改了名称， 需要检查名称重复.
                        if (IsExistsStockPoolName(stockPool.OrganizationID, stockPool.StockPoolName))
                        {
                            // 股票池名称已存在.
                            return WorkServiceResult.StockPoolNameHadExistsResult;
                        }
                    }

                    // 更新.
                    base.UpdateProperty<StockPool>(stockPool, data);
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

        /// <summary>
        /// 删除股票池.
        /// </summary>
        /// <param name="stockPoolID"></param>
        /// <returns></returns>
        CommonServiceResult IStockPoolService.DeleteStockPool(long organizationID, long stockPoolID)
        {
            try
            {
                //using (MyWorkContext context = new MyWorkContext())
                {
                    // 查询.
                    StockPool data = context.StockPools.Find(stockPoolID);

                    if(data == null)
                    {
                        // 数据不存在.
                        return WorkServiceResult.StockPoolIDNotFoundResult;
                    }
                    if(data.OrganizationID != organizationID)
                    {
                        // 组织ID不匹配.
                        return AuthenticationServiceResult.OrganizationNotMatchResult;
                    }

                    // 删除.
                    context.StockPools.Remove(data);
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



        /// <summary>
        /// 获取指定股票池中的股票列表.
        /// </summary>
        /// <param name="stockPoolID"></param>
        /// <returns></returns>
        CommonQueryResult<StockInfo> IStockPoolService.GetStockInfoInPool(long organizationID, long stockPoolID, int pageNo, int pageSize)
        {
            //using (MyWorkContext context = new MyWorkContext())
            {
                var checkResult = BasicOrganizationCheck(organizationID, stockPoolID);
                if (!checkResult.IsSuccess)
                {
                    // 检查失败.
                    // 返回空白列表.
                    return CommonQueryResult<StockInfo>.GetEmptyResult();
                }

                var query =
                    from data in context.StockPoolDetails
                    where
                        data.StockPoolID == stockPoolID
                    select
                        data.Stock;

                // 初始化翻页.
                PageInfo pgInfo = new PageInfo(
                    pageSize: pageSize,
                    pageNo: pageNo,
                    rowCount: query.Count());

                // 翻页.
                query = query.OrderBy(p => p.StockCode)
                    .Skip(pgInfo.SkipValue)
                    .Take(pgInfo.PageSize);

                List<StockInfo> dataList = query.ToList();

                CommonQueryResult<StockInfo> result = new CommonQueryResult<StockInfo>()
                {
                    QueryPageInfo = pgInfo,
                    QueryResultData = dataList
                };

                return result;
            }
        }


        /// <summary>
        /// 获取股票池数据.
        /// </summary>
        /// <param name="stockPoolID"></param>
        /// <returns></returns>
        CommonServiceResult IStockPoolService.GetStockPool(long organizationID, long stockPoolID)
        {
            try
            {
                //using (MyWorkContext context = new MyWorkContext())
                {
                    // 查询.
                    StockPool data = context.StockPools.Find(stockPoolID);

                    if (data == null)
                    {
                        // 数据不存在.
                        return WorkServiceResult.StockPoolIDNotFoundResult;
                    }
                    if (data.OrganizationID != organizationID)
                    {
                        // 组织ID不匹配.
                        return AuthenticationServiceResult.OrganizationNotMatchResult;
                    }

                    CommonServiceResult result = CommonServiceResult.CreateDefaultSuccessResult(data);

                    // 返回
                    return result;
                }
            }
            catch (Exception ex)
            {
                return new CommonServiceResult(ex);
            }
        }



        /// <summary>
        /// 获取股票池列表.
        /// </summary>
        /// <param name="organizationID"></param>
        /// <returns></returns>
        CommonQueryResult<StockPool> IStockPoolService.GetStockPoolList(long organizationID, int pageNo, int pageSize)
        {
            //using (MyWorkContext context = new MyWorkContext())
            {
                var query =
                    from data in context.StockPools
                    where
                        data.OrganizationID == organizationID
                    select
                        data;

                // 初始化翻页.
                PageInfo pgInfo = new PageInfo(
                    pageSize: pageSize,
                    pageNo: pageNo,
                    rowCount: query.Count());

                // 翻页.
                query = query.OrderByDescending(p => p.LastUpdateTime)
                    .Skip(pgInfo.SkipValue)
                    .Take(pgInfo.PageSize);

                List<StockPool> dataList = query.ToList();

                CommonQueryResult<StockPool> result = new CommonQueryResult<StockPool>()
                {
                    QueryPageInfo = pgInfo,
                    QueryResultData = dataList
                };

                return result;
            }
        }


        /// <summary>
        /// 向股票池中添加股票数据.
        /// </summary>
        /// <param name="organizationID"></param>
        /// <param name="stockPoolID"></param>
        /// <param name="stockCode"></param>
        /// <returns></returns>
        CommonServiceResult IStockPoolService.AddStockToPool(long organizationID, long stockPoolID, string stockCode)
        {
            try
            {
                //using (MyWorkContext context = new MyWorkContext())
                {
                    var checkResult = BasicOrganizationCheck(organizationID, stockPoolID);
                    if (!checkResult.IsSuccess)
                    {
                        // 检查失败.
                        return checkResult;
                    }


                    var query =
                        from data in context.StockPoolDetails
                        where
                            data.StockPoolID == stockPoolID
                            && data.StockCode == stockCode
                        select data;

                    int rowCount = query.Count();

                    if(rowCount > 0)
                    {
                        // 数据已存在，不需要重复添加.
                        return WorkServiceResult.StockCodeHadExistsResult;
                    }

                    // 新增的操作.
                    StockPoolDetail newData = new StockPoolDetail();
                    newData.StockPoolID = stockPoolID;
                    newData.StockCode = stockCode;

                    // 开始处理.
                    context.StockPoolDetails.Add(newData);
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


        /// <summary>
        /// 从股票池中移除股票数据
        /// </summary>
        /// <param name="organizationID"></param>
        /// <param name="stockPoolID"></param>
        /// <param name="stockCode"></param>
        /// <returns></returns>
        CommonServiceResult IStockPoolService.RemoveStockFromPool(long organizationID, long stockPoolID, string stockCode)
        {
            try
            {
                //using (MyWorkContext context = new MyWorkContext())
                {
                    var checkResult = BasicOrganizationCheck(organizationID, stockPoolID);
                    if (!checkResult.IsSuccess)
                    {
                        // 检查失败.
                        return checkResult;
                    }


                    var query =
                        from data in context.StockPoolDetails
                        where
                            data.StockPoolID == stockPoolID
                            && data.StockCode == stockCode
                        select data;

                    var dbData = query.FirstOrDefault();

                    if (dbData == null)
                    {
                        // 数据不存在
                        return WorkServiceResult.StockCodeNotFoundResult;
                    }

                    // 开始处理.
                    context.StockPoolDetails.Remove(dbData);
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

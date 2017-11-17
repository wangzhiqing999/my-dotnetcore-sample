using MyMiniTradingSystem.Model;




namespace MyMiniTradingSystem.Service
{

    /// <summary>
    /// 产品行情服务.
    /// </summary>
    public interface ICommodityPriceService
    {
        /// <summary>
        /// 更新每日行情.
        /// </summary>
        /// <param name="newData"></param>
        /// <returns></returns>
        ServiceResult InsertOrUpdateCommodityPrice(CommodityPrice newData);
    }

}
using MyMiniTradingSystem.Model;

namespace MyMiniTradingSystem.Service
{


    /// <summary>
    /// 仓位服务.
    /// </summary>
    public interface IPositionService
    {

        /// <summary>
        /// 建仓.
        /// </summary>
        /// <param name="newData"></param>
        /// <returns></returns>
        ServiceResult OpenPosition(Position newData);


        /// <summary>
        /// 平仓.
        /// </summary>
        /// <param name="newData"></param>
        /// <returns></returns>
        ServiceResult ClosePosition(Position newData);
        
    }
}
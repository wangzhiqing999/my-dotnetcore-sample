using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;


using P0002_MyTrading.Model;
using P0002_MyTrading.DataAccess;
using P0002_MyTrading.Service;
using P0002_MyTrading.ServiceModel;

namespace P0002_MyTrading.ServiceImpl
{
    public class DefaultSimpleTradingService : ISimpleTradingService
    {



        private readonly MyTradingContext _MyTradingContext;


        private readonly ILogger<DefaultSimpleTradingService> _Logger;


        public DefaultSimpleTradingService(MyTradingContext context, ILogger<DefaultSimpleTradingService> logger)
        {
            this._MyTradingContext = context;
            this._Logger = logger;
        }



        public ServiceResult DoOpen(TradingRequest tradingRequest)
        {
            if (this._Logger.IsEnabled(LogLevel.Debug))
            {
                this._Logger.LogDebug($"DoOpen Start. Request=[ {tradingRequest} ]");
            }

            try
            {
                var checkQuery =
                    from data in _MyTradingContext.SimpleTradings
                    where
                        data.TradingItemCode == tradingRequest.TradingItemCode
                        && data.CloseDate == null
                    select
                        data;

                SimpleTrading simpleTrading = checkQuery.FirstOrDefault();
                if(simpleTrading != null)
                {
                    return ServiceResult.DataHadExistsResult;
                }
                simpleTrading = new SimpleTrading()
                {
                    TradingItemCode = tradingRequest.TradingItemCode,
                    TradingQuantity = tradingRequest.TradingQuantity,

                    OpenDate = tradingRequest.TradingDate,
                    OpenPrice = tradingRequest.TradingPrice
                };

                _MyTradingContext.SimpleTradings.Add(simpleTrading);
                _MyTradingContext.SaveChanges();


                return ServiceResult.DefaultSuccessResult;

            }
            catch (Exception ex)
            {
                this._Logger.LogError(ex, ex.Message);
                return new ServiceResult(ex);
            }

        }


        public ServiceResult DoClose(TradingRequest tradingRequest)
        {
            if (this._Logger.IsEnabled(LogLevel.Debug))
            {
                this._Logger.LogDebug($"DoClose Start. Request=[ {tradingRequest} ]");
            }


            try
            {
                var checkQuery =
                    from data in _MyTradingContext.SimpleTradings
                    where
                        data.TradingItemCode == tradingRequest.TradingItemCode
                        && data.TradingQuantity == tradingRequest.TradingQuantity
                        && data.CloseDate == null
                    select
                        data;

                SimpleTrading simpleTrading = checkQuery.FirstOrDefault();
                if (simpleTrading == null)
                {
                    return ServiceResult.DataNotFoundResult;
                }

                simpleTrading.CloseDate = tradingRequest.TradingDate;
                simpleTrading.ClosePrice = tradingRequest.TradingPrice;
                
                _MyTradingContext.SaveChanges();


                return ServiceResult.DefaultSuccessResult;

            }
            catch (Exception ex)
            {
                this._Logger.LogError(ex, ex.Message);
                return new ServiceResult(ex);
            }


        }

        public bool HasPosition(string tradingItemCode)
        {
            try
            {
                var checkQuery =
                    from data in _MyTradingContext.SimpleTradings
                    where
                        data.TradingItemCode == tradingItemCode
                        && data.CloseDate == null
                    select
                        data;

                SimpleTrading simpleTrading = checkQuery.FirstOrDefault();
                if (simpleTrading == null)
                {
                    // 无数据， 意味着  没有持仓.
                    return false;
                }
                
                // 有数据，意味着有持仓.
                return true;

            }
            catch (Exception ex)
            {
                this._Logger.LogError(ex, ex.Message);
                return false;
            }
        }
    }
}

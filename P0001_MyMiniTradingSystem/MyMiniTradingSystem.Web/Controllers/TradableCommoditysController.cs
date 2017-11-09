using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using MyMiniTradingSystem.Model;
using MyMiniTradingSystem.Service;


namespace MyMiniTradingSystem.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/TradableCommoditys")]
    public class TradableCommoditysController : Controller
    {

        private ITradableCommodityService _TradableCommodityService;

        public TradableCommoditysController(ITradableCommodityService tradableCommodityService)
        {
            _TradableCommodityService = tradableCommodityService;
        }

        // GET: api/TradableCommoditys
        [HttpGet]
        public IEnumerable<TradableCommodity> Get()
        {
            var result = this._TradableCommodityService.GetAll();

            return result;
        }

        // GET: api/TradableCommoditys/5
        [HttpGet("{id}", Name = "Get")]
        public TradableCommodity Get(string id)
        {
            var result = this._TradableCommodityService.Get(id);

            return result;
        }

    }
}

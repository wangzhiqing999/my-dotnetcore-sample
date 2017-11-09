using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyMiniTradingSystem.DataAccess;
using MyMiniTradingSystem.Model;

namespace MyMiniTradingSystem.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/CommodityPrices")]
    public class CommodityPricesController : Controller
    {
        private readonly MyMiniTradingSystemContext _context;

        public CommodityPricesController(MyMiniTradingSystemContext context)
        {
            _context = context;
        }

        // GET: api/CommodityPrices
        [HttpGet]
        public IEnumerable<CommodityPrice> GetCommodityPrices()
        {
            return _context.CommodityPrices;
        }

        // GET: api/CommodityPrices/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCommodityPrice([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var commodityPrice = await _context.CommodityPrices.SingleOrDefaultAsync(m => m.CommodityCode == id);

            if (commodityPrice == null)
            {
                return NotFound();
            }

            return Ok(commodityPrice);
        }

        // PUT: api/CommodityPrices/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCommodityPrice([FromRoute] string id, [FromBody] CommodityPrice commodityPrice)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != commodityPrice.CommodityCode)
            {
                return BadRequest();
            }

            _context.Entry(commodityPrice).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CommodityPriceExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/CommodityPrices
        [HttpPost]
        public async Task<IActionResult> PostCommodityPrice([FromBody] CommodityPrice commodityPrice)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.CommodityPrices.Add(commodityPrice);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CommodityPriceExists(commodityPrice.CommodityCode))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetCommodityPrice", new { id = commodityPrice.CommodityCode }, commodityPrice);
        }

        // DELETE: api/CommodityPrices/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCommodityPrice([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var commodityPrice = await _context.CommodityPrices.SingleOrDefaultAsync(m => m.CommodityCode == id);
            if (commodityPrice == null)
            {
                return NotFound();
            }

            _context.CommodityPrices.Remove(commodityPrice);
            await _context.SaveChangesAsync();

            return Ok(commodityPrice);
        }

        private bool CommodityPriceExists(string id)
        {
            return _context.CommodityPrices.Any(e => e.CommodityCode == id);
        }
    }
}
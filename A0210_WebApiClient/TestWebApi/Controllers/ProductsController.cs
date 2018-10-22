using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace TestWebApi.Controllers
{
    [Route("api/[controller]")]
    public class ProductsController : Controller
    {

        private Product[] products = new Product[]
        {
            new Product { Id = 1, Name = "可口可乐", Category = "碳酸饮料", Price = 48 },
            new Product { Id = 2, Name = "蒙牛纯牛奶", Category = "牛奶乳品", Price = 36.90M },
            new Product { Id = 3, Name = "雀巢咖啡", Category = "咖啡", Price = 99 }
        };



        // GET api/Products
        [HttpGet]
        public IEnumerable<Product> Get()
        {
            return products;
        }

        // GET api/Products/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var item = products.FirstOrDefault(p => p.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }
        
    }



    /// <summary>
    /// 产品.
    /// </summary>
    public class Product
    {
        /// <summary>
        /// 产品代码.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 产品名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 产品分类.
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// 产品价格.
        /// </summary>
        public decimal Price { get; set; }

    }

}

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiClient;
using WebApiClient.Attributes;

namespace TestWebApiClient
{

    /// <summary>
    /// 产品服务.
    /// </summary>
    [TraceFilter]
    public interface IProductService : IHttpApi
    {

        /// <summary>
        /// 获取全部产品.
        /// </summary>
        /// <returns></returns>
        [HttpGet("/api/Products")]
        ITask<List<Product>> GetProductsAsync();



        /// <summary>
        /// 获取指定产品.
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        [HttpGet("/api/Products/{id}")]
        ITask<Product> GetProductAsync(int id);

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

using System;
using Refit;


namespace RefitClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var productService = RestService.For<IProductService>("http://localhost:9090");

            var products = productService.GetProductsAsync().GetAwaiter().GetResult();

            Console.WriteLine("GetProductsAsync result:");
            foreach (var item in products)
            {
                Console.WriteLine($"{item.Id}--{item.Name}--{item.Category}--{item.Price}");
            }

            Console.WriteLine();
            var product = productService.GetProductAsync(1).GetAwaiter().GetResult();
            Console.WriteLine("GetProductAsync result:");
            Console.WriteLine($"{product.Id}-{product.Name}");

            /*
            注：搜索一个不存在的数据， 服务器端返回 404 NotFound.  客户端这里抛异常了。
            Console.WriteLine();
            product = productApi.GetProductAsync(99999).GetAwaiter().GetResult();
            Console.WriteLine("GetProductAsync result:");
            Console.WriteLine(product);
            */


            Console.WriteLine("Finish!");
            Console.ReadLine();
        }
    }
}


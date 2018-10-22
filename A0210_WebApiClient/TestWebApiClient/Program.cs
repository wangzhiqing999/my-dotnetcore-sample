using System;
using WebApiClient;


namespace TestWebApiClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = new HttpApiConfig
            {
                HttpHost = new Uri("http://localhost:9090"),
            };

            using (var productService = HttpApiClient.Create<IProductService>(config))
            {
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
            }

            Console.WriteLine("Finish!");
            Console.ReadLine();
        }
    }
}

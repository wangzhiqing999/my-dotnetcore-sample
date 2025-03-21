using DifyAI.ObjectModels;
using DifyAI.Interfaces;

using Microsoft.Extensions.DependencyInjection;
using System.Reflection.Metadata;
using System.Text.Json;


namespace AI003_Dify
{
    internal class Program
    {


        public const string DIFY_URL = "http://192.168.1.241/v1";

        public const string DIFY_API_KEY = "app-x9DZrG563coETAiqqjqrBup7";



        static async Task Main(string[] args)
        {


            var services = new ServiceCollection();            
            services.AddDifyAIService(x =>
                {
                    x.BaseDomain = DIFY_URL;
                    x.DefaultApiKey = DIFY_API_KEY;
                });

            var app = services.BuildServiceProvider();

            IDifyAIService difyAIService = app.GetRequiredService<IDifyAIService>();
            await TestWorkflow(difyAIService);

            Console.WriteLine("Finish!");
            Console.ReadKey();
        }









        /// <summary>
        /// 测试工作流.
        /// </summary>
        static async Task TestWorkflow(IDifyAIService difyAIService)
        {
            Console.WriteLine("--- 测试调用 Dify 的工作流 ---");


            // 定义工作流的请求参数
            CompletionRequest request = new CompletionRequest();

            request.ApiKey = DIFY_API_KEY;
            request.Inputs = new Dictionary<string, string>();
            request.Inputs.Add("place","上海");
            request.User = "test-001";



            // 阻塞模式
            var rsp = await difyAIService.Workflows.WorkflowAsync(request);

            // 获取工作流的返回结果.
            JsonDocument document = rsp.Data.Outputs;



            // 输出整个结果的 json 字符串.
            using (var stream = new System.IO.MemoryStream())
            using (var writer = new Utf8JsonWriter(stream))
            {
                // 将 JsonDocument 内容写入 Utf8JsonWriter
                document.WriteTo(writer);
                // 刷新写入器以确保所有数据都被写入流
                writer.Flush();

                // 将流中的数据转换为字符串
                string jsonOutput = System.Text.Encoding.UTF8.GetString(stream.ToArray());

                // 输出 JSON 字符串
                Console.WriteLine(jsonOutput);
            }




            // 输出结果的 json 中的 "output" 属性.
            // 获取根元素
            JsonElement root = document.RootElement;
            // 尝试获取 "output" 属性的值
            if (root.TryGetProperty("output", out JsonElement outputElement))
            {
                // 检查属性值是否为字符串类型
                if (outputElement.ValueKind == JsonValueKind.String)
                {
                    string? output = outputElement.GetString();
                    Console.WriteLine($"output: {output}");
                }
            }


        }


    }
}

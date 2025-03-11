using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OllamaSharp;
using OllamaSharp.Models;


namespace AI002_Ollama
{
    class OllamaSharpTest
    {


        public static async void DoTest()
        {

            Console.WriteLine("===== 测试使用 OllamaSharp ===== ");


            // 初始化.
            var uri = new Uri(Constant.OLLAMA_HOST);
            var ollama = new OllamaApiClient(uri);



            Console.WriteLine("本地模型列表:");
            var models = await ollama.ListLocalModelsAsync();
            foreach (var model in models)
            {
                Console.WriteLine(model.Name);
            }



            Console.WriteLine("测试简单的提问.");
            ollama.SelectedModel = Constant.OLLAMA_MODEL;
            await foreach (var stream in ollama.GenerateAsync("你是谁?"))
                Console.Write(stream.Response);


        }


        public static async void DoTest2()
        {
            Console.WriteLine("===== 测试使用 OllamaSharp ===== ");

            // 初始化.
            var uri = new Uri(Constant.OLLAMA_HOST);
            var ollama = new OllamaApiClient(uri);

            GenerateRequest request = new GenerateRequest()
            {
                System = "你是一个软件工程师",
                Prompt = "写一段Python代码，用来解码base64编码的字符串",
                Model = Constant.OLLAMA_MODEL,
                Stream = true
            };

            await foreach (var stream in ollama.GenerateAsync(request))
                Console.Write(stream.Response);
        }
    }
}

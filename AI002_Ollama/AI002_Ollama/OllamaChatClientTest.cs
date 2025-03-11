using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Extensions.AI;


namespace AI002_Ollama
{
    class OllamaChatClientTest
    {

        public static async void DoTest()
        {
            Console.WriteLine("===== 测试使用 Microsoft.Extensions.AI.Ollama ===== ");

            IChatClient client = new OllamaChatClient(new Uri(Constant.OLLAMA_HOST), Constant.OLLAMA_MODEL);

            await foreach (var update in client.GetStreamingResponseAsync("你是谁?"))
            {
                Console.Write(update);
            }
        }



        public static async void DoTest2()
        {

            Console.WriteLine("===== 测试使用 Microsoft.Extensions.AI.Ollama ===== ");

            IChatClient client = new OllamaChatClient(new Uri(Constant.OLLAMA_HOST), Constant.OLLAMA_MODEL);


            ChatMessage[] chatMessages = [
                new ChatMessage(ChatRole.System, "你是一个软件工程师"),
                new ChatMessage(ChatRole.User, "写一段Python代码，用来解码base64编码的字符串"),
            ];



            // 如果是前端的处理. AI 生成一点，就输出一点，可以用下面这种写法。
            await foreach (var update in client.GetStreamingResponseAsync(chatMessages))
            {
                Console.Write(update);
            }


            // 如果是后台作业的处理，不看中间过程的，最后结果存文件或者发邮件的，就可以用下面这种写法。
            /*
            Console.WriteLine(await client.GetResponseAsync(chatMessages));
            */
        }

    }
}

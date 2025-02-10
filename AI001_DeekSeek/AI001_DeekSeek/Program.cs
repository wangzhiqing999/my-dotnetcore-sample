using Fitomad.DeepSeek;
using Fitomad.DeepSeek.Endpoints.Chat;
using Fitomad.DeepSeek.Entities.Chat;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpenAI;
using OpenAI.Chat;
using System.ClientModel;
using System.ClientModel.Primitives;
using System.Runtime;



namespace AI001_DeekSeek
{
    internal class Program
    {
        static void Main(string[] args)
        {

            TestByOpenAI();

            TestByFitomad();

            Console.ReadLine();
        }




        // https://api-docs.deepseek.com/zh-cn/
        // * deepseek-chat 模型已全面升级为 DeepSeek-V3，接口不变。
        // 通过指定 model='deepseek-chat' 即可调用 DeepSeek-V3。

        // * deepseek-reasoner 是 DeepSeek 最新推出的推理模型 DeepSeek-R1。
        // 通过指定 model = 'deepseek-reasoner'，即可调用 DeepSeek-R1。





        static void TestByOpenAI()
        {
            Console.WriteLine("---------- TestByOpenAI ----------");

            var configuration = new ConfigurationBuilder()
               .AddEnvironmentVariables()
               .AddUserSecrets<Program>()
               .Build();

            var _apiKey = configuration.GetValue<string>("DeepSeek:ApiKey");
            ApiKeyCredential cred = new ApiKeyCredential(_apiKey);


            
            ChatClient client = new ChatClient(
                // 这个是 V3.
                "deepseek-chat",
                // 这个是 R1.
                // "deepseek-reasoner",
                cred, 
                new OpenAIClientOptions
                {
                    Endpoint = new Uri("https://api.deepseek.com"),
                    UserAgentApplicationId = "webmote",
                    ProjectId = "deepseek-test",
                    RetryPolicy = ClientRetryPolicy.Default
                });

            List<ChatMessage> messages = new List<ChatMessage>()
            {
                // new SystemChatMessage("You are a helpful assistant."),
                new SystemChatMessage(""),
                new UserChatMessage("你是谁？")
            };

            var result = client.CompleteChat(messages);
            if (result?.Value != null)
            {
                Console.WriteLine(result.Value.Content[0].Text);
            }
        }




        static void TestByFitomad()
        {
            Console.WriteLine("---------- TestByFitomad ----------");

            var configuration = new ConfigurationBuilder()
               .AddEnvironmentVariables()
               .AddUserSecrets<Program>()
               .Build();

            var _apiKey = configuration.GetValue<string>("DeepSeek:ApiKey");

            var deepSeekSettings = new DeepSeekSettingsBuilder()
                .WithApiKey(_apiKey)
                .Build();


            var services = new ServiceCollection();
            services.AddDeepSeekHttpClient(settings: deepSeekSettings);


            IDeepSeekClient _deepSeekClient = services.BuildServiceProvider().GetRequiredService<IDeepSeekClient>();


            ChatRequest request = new ChatRequestBuilder()
                // 这个是 V3.
                .WithModel(ChatModelType.DeepSeekChat)
                // 这个是 R1.
                // .WithModel(ChatModelType.DeepSeekReasoner)
                .WithSystemMessage("")
                .WithUserMessage("你是谁？")
                .Build();

            var resp = _deepSeekClient.ChatCompletion.CreateChatAsync(request).Result;

            Console.WriteLine(resp.Choices[0].ReceivedMessage.Content);

        }

    }
}

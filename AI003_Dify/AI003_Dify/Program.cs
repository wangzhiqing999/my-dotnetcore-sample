using DifyAI.ObjectModels;
using DifyAI.Interfaces;

using Microsoft.Extensions.DependencyInjection;
using System.Reflection.Metadata;
using System.Text.Json;
using System.Net;
using System.Text;


namespace AI003_Dify
{
    internal class Program
    {

        public const string HOST_URL = "http://m710q";

        public const string DIFY_URL = "http://m710q/v1";

        public const string DIFY_API_KEY = "app-HBoZodU6SAPw2GURf6sfriAu";



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


            // await TestWorkflow(difyAIService);


            // await TestWorkflow2(difyAIService);


            // await TestAgent(difyAIService);

            await TestAgentWithUploadFile(difyAIService);


            Console.WriteLine("Finish!");
            Console.ReadKey();
        }






        #region 普通的工作流测试


        /// <summary>
        /// 测试普通的工作流.
        /// <br/>
        /// 单纯测试 返回字符串信息的工作流
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



        #endregion





        #region 返回文件的工作流测试.


        public const string DIFY_API_KEY2 = "app-hPY8nrFExoWoqfYvGKZkjG4r";



        /// <summary>
        /// 这里是测试，返回结果是文件的工作流.
        /// <br/>
        /// 结果的json中，对于结果文件，
        /// 一个属性是 filename，这个文件，
        /// 位于发布 Dify 的那台机器的 /dify/docker/volumes/app/storage/tools/decaff41-8654-4e29-9806-e0c47cb28bea 目录下.
        /// <br/>
        /// 
        /// 另外一个属性是 url 的后半部分，没有机器名.
        /// 
        /// 尝试 http://测试机器名 拼接上 url ，
        /// 直接在浏览器上访问，结果返回
        /// {"code": "forbidden", "message": "Invalid request.", "status": 403}
        /// 
        /// 然后，用代码下载的时候，需要 http header 中设置
        /// Authorization: Bearer api key
        /// 才能正常的下载.
        /// 
        /// </summary>
        /// <param name="difyAIService"></param>
        /// <returns></returns>
        static async Task TestWorkflow2(IDifyAIService difyAIService)
        {
            Console.WriteLine("--- 测试调用 Dify 的工作流 ---");


            // 定义工作流的请求参数
            CompletionRequest request = new CompletionRequest();

            request.ApiKey = DIFY_API_KEY2;
            request.Inputs = new Dictionary<string, string>();
            request.Inputs.Add("inputText", "在 windows 环境下，如何计算文件的 sha256。");
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
            // 尝试获取 "pdfresult" 属性的值
            if (root.TryGetProperty("pdfresult", out JsonElement pdfresultElement))
            {
                // 检查属性值是否为字符串类型
                if (pdfresultElement.ValueKind == JsonValueKind.Array)
                {

                    foreach (var item in pdfresultElement.EnumerateArray())
                    {
                        string? filename = null;
                        string? url = null;

                        if (item.TryGetProperty("filename", out JsonElement filenameElement) && filenameElement.ValueKind == JsonValueKind.String)
                        {
                            filename = filenameElement.GetString();
                            Console.WriteLine($"filename = {filename}");
                        }

                        if (item.TryGetProperty("url", out JsonElement urlElement) && urlElement.ValueKind == JsonValueKind.String)
                        {
                            url = urlElement.GetString();
                            Console.WriteLine($"url = {url}");
                        }

                        if(filename != null && url != null)
                        {
                            DownloadWorkflowResultFile(url, filename);
                        }
                        
                    }
                }
            }

            // 尝试获取 "pdfresult" 属性的值
            if (root.TryGetProperty("wordresult", out JsonElement wordresultElement))
            {
                foreach (var item in wordresultElement.EnumerateArray())
                {
                    string? filename = null;
                    string? url = null;

                    if (item.TryGetProperty("filename", out JsonElement filenameElement) && filenameElement.ValueKind == JsonValueKind.String)
                    {
                        filename = filenameElement.GetString();
                        Console.WriteLine($"filename = {filename}");
                    }

                    if (item.TryGetProperty("url", out JsonElement urlElement) && urlElement.ValueKind == JsonValueKind.String)
                    {
                        url = urlElement.GetString();
                        Console.WriteLine($"url = {url}");
                    }

                    if (filename != null && url != null)
                    {
                        DownloadWorkflowResultFile(url, filename);
                    }
                }
            }
        }




        static void DownloadWorkflowResultFile(string url, string filename)
        {
            try
            {
                // 创建 WebRequest 对象
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create($"{HOST_URL}{url}");
                // 设置请求方法为 GET
                request.Method = "GET";
                // 添加 Authorization 请求头
                request.Headers.Add("Authorization", $"Bearer {DIFY_API_KEY2}");

                // 发送请求并获取响应
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    // 检查响应状态码是否为成功
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        // 获取响应流
                        using (Stream responseStream = response.GetResponseStream())
                        {
                            // 创建文件流以写入下载的数据
                            using (FileStream fileStream = new FileStream(filename, FileMode.Create, FileAccess.Write))
                            {
                                byte[] buffer = new byte[4096];
                                int bytesRead;
                                // 从响应流读取数据并写入文件流
                                while ((bytesRead = responseStream.Read(buffer, 0, buffer.Length)) > 0)
                                {
                                    fileStream.Write(buffer, 0, bytesRead);
                                }
                            }
                        }
                        Console.WriteLine("文件下载成功！");
                    }
                    else
                    {
                        Console.WriteLine($"下载失败，状态码: {response.StatusCode}");
                    }
                }
            }
            catch (WebException ex)
            {
                Console.WriteLine($"Web 异常: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"发生异常: {ex.Message}");
            }
        }


        #endregion






        #region Agent 测试.


        public const string DIFY_API_KEY3 = "app-9fyy30Cb9xvrX6fyTaTXLLqR";


        static async Task TestAgent(IDifyAIService difyAIService)
        {
            Console.WriteLine("--- 测试调用 Dify 的消息审核的 Agent ---");


            // 定义工作流的请求参数
            ChatCompletionRequest request = new ChatCompletionRequest();

            request.ApiKey = DIFY_API_KEY3;
            request.User = "test-001";
            request.Query = "老师好，请问 黄金ETF，现在能买入么？";

            // 阻塞模式
            // var rsp = await difyAIService.ChatMessages.ChatAsync(request);

            Console.WriteLine($"发出请求：{request.Query}");

            StringBuilder answerBuff = new StringBuilder();
            // 流式模式
            await foreach (var rsp in difyAIService.ChatMessages.ChatStreamAsync(request))
            {
                if(rsp != null && rsp.Event == "agent_message")
                {
                    ChunkCompletionAgentMessageResponse? agentResp = rsp as ChunkCompletionAgentMessageResponse;
                    if(agentResp != null)
                    {
                        string answer = agentResp.Answer;                        
                        answerBuff.Append(answer);
                    }
                }
            }
            string answerResult = answerBuff.ToString();
            answerResult = answerResult.Trim(' ', '`');
            Console.WriteLine(answerResult);




            request.Query = "什么玩意，你推荐的股票，都是一些什么垃圾？";
            Console.WriteLine($"发出请求：{request.Query}");
            // 阻塞模式
            // rsp = await difyAIService.ChatMessages.ChatAsync(request);
            // 获取返回应答.
            // resp = rsp.Answer;
            // Console.WriteLine($"{request.Query} -- {resp[0]}");


            answerBuff = new StringBuilder();
            await foreach (var rsp in difyAIService.ChatMessages.ChatStreamAsync(request))
            {
                if (rsp != null && rsp.Event == "agent_message")
                {
                    ChunkCompletionAgentMessageResponse? agentResp = rsp as ChunkCompletionAgentMessageResponse;
                    if (agentResp != null)
                    {
                        string answer = agentResp.Answer;
                        answerBuff.Append(answer);
                    }
                }
            }
            answerResult = answerBuff.ToString();
            answerResult = answerResult.Trim(' ', '`');
            Console.WriteLine(answerResult);



        }



        #endregion






        #region 参数中包含文件的测试.


        public const string DIFY_API_KEY4 = "app-cuxD7JUAJ3MGTLnOV4Zk5yq7";



        /// <summary>
        /// 测试照片识别的工作流.
        /// <br/>
        /// 特点在于，参数是文件，需要做一个文件上传.
        /// </summary>
        static async Task TestWorkflowWithUploadFile(IDifyAIService difyAIService)
        {
            Console.WriteLine("--- 测试调用 Dify 的身份证识别的工作流 ---");


            var uploadReq = new FileUploadRequest();
            uploadReq.ApiKey = DIFY_API_KEY4;
            uploadReq.File = "test_id_card.jpg";
            uploadReq.User = "test-004";


            var uploadResult = difyAIService.Files.UploadAsync(uploadReq);
            FileUploadResponse uploadRespose = uploadResult.Result;


            Console.WriteLine(@$"上传文件 {uploadReq.File}
结果：ID = {uploadRespose.Id}; Name = {uploadRespose.Name};Size = {uploadRespose.Size}");


            // 定义工作流的请求参数
            CompletionRequest request = new CompletionRequest();


            CompletionFile completionFile = new CompletionFile()
            {
                Type = "image",
                TransferMethod = "local_file",
                UploadFileId = uploadRespose.Id
            };
            var requestFiles = new List<CompletionFile>();
            requestFiles.Add(completionFile);
            request.Files = requestFiles;



            request.ApiKey = DIFY_API_KEY4;
            request.Inputs = new Dictionary<string, string>();
            request.Inputs.Add("id_card_image", uploadRespose.Id);
            request.User = "test-004";





            // 工作流中，参数是图片的情况下
            // 上面的 Inputs 是 Dictionary<string, string>() 的。
            // 无论是填写名称，还是文件ID，还是 json 序列化，都是要报错的。


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




            // 输出结果的 json 中的 "result" 属性.
            // 获取根元素
            JsonElement root = document.RootElement;
            // 尝试获取 "result" 属性的值
            if (root.TryGetProperty("result", out JsonElement outputElement))
            {
                // 检查属性值是否为字符串类型
                if (outputElement.ValueKind == JsonValueKind.String)
                {
                    string? output = outputElement.GetString();
                    Console.WriteLine($"result: {output}");
                }
            }


        }






        public const string DIFY_API_KEY5 = "app-wgcAhKEWwW4PpnOsAMrFD2bo";


        static async Task TestAgentWithUploadFile(IDifyAIService difyAIService)
        {
            Console.WriteLine("--- 测试调用 Dify 的身份证识别的 Agent ---");


            var uploadReq = new FileUploadRequest();
            uploadReq.ApiKey = DIFY_API_KEY5;
            uploadReq.File = "test_id_card.jpg";
            uploadReq.User = "test-004";

            var uploadResult = difyAIService.Files.UploadAsync(uploadReq);
            FileUploadResponse uploadRespose = uploadResult.Result;


            Console.WriteLine(@$"上传文件 {uploadReq.File}
结果：ID = {uploadRespose.Id}; Name = {uploadRespose.Name};Size = {uploadRespose.Size}");




            // 定义工作流的请求参数
            ChatCompletionRequest request = new ChatCompletionRequest();

            CompletionFile completionFile = new CompletionFile()
            {
                Type = "image",
                TransferMethod = "local_file",
                UploadFileId = uploadRespose.Id
            };
            var requestFiles = new List<CompletionFile>();
            requestFiles.Add(completionFile);
            request.Files = requestFiles;

            request.ApiKey = DIFY_API_KEY5;
            request.User = "test-005";
            request.Query = "看一下这个图片";
            

            // 阻塞模式
            // var rsp = await difyAIService.ChatMessages.ChatAsync(request);

            Console.WriteLine($"发出请求.");

            StringBuilder answerBuff = new StringBuilder();
            // 流式模式
            await foreach (var rsp in difyAIService.ChatMessages.ChatStreamAsync(request))
            {
                if (rsp != null && rsp.Event == "agent_message")
                {
                    ChunkCompletionAgentMessageResponse? agentResp = rsp as ChunkCompletionAgentMessageResponse;
                    if (agentResp != null)
                    {
                        string answer = agentResp.Answer;
                        answerBuff.Append(answer);
                    }
                }
            }
            string answerResult = answerBuff.ToString();
            answerResult = answerResult.Trim(' ', '`');
            Console.WriteLine(answerResult);


        }


        #endregion



    }
}

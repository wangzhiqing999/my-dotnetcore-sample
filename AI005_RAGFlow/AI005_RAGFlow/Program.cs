using Microsoft.Extensions.DependencyInjection;
using RAGFlowSharp.Api;
using RAGFlowSharp.Dtos.ChatAssistant;
using RAGFlowSharp.Dtos.File;
using RAGFlowSharp.Dtos.Session;


namespace AI005_RAGFlow
{
    internal class Program
    {

        public const string RAGFLOW_URL = "http://192.168.1.18";

        public const string RAGFLOW_API_KEY = "ragflow-VkMDMyMWY4M2EwZjExZjBhYTc4MDZhY2";




        static async Task Main(string[] args)
        {
            var services = new ServiceCollection();

            services.AddRagflowSharp(options =>
            {
                // Configuration options
                options.BaseUrl  = RAGFLOW_URL;
                options.ApiKey = RAGFLOW_API_KEY;
            });


            var app = services.BuildServiceProvider();
            var ragflow = app.GetRequiredService<IRagflowApi>();


            await TestListDatasets(ragflow);

            // await TestAll(ragflow);

            // await TestChatAssistant(ragflow);


            Console.WriteLine("Finish!");
            Console.ReadKey();
        }



        /// <summary>
        /// 查询 知识库. 以及知识库下面的文件.
        /// 查询 聊天助手. 以及聊天助手下面的会话.
        /// 
        /// 结果是查询会话的时候，返回 404.
        /// </summary>
        /// <param name="ragflowApi"></param>
        /// <returns></returns>
        static async Task TestListDatasets(IRagflowApi ragflowApi)
        {
            // 列出已有的知识库.

            Console.WriteLine("--- 测试获取 RAGFlow 的知识库 ---");

            var datasets = await ragflowApi.ListDatasets();
            foreach (var dataset in datasets.Data)
            {
                Console.WriteLine($"知识库: name={dataset.Name}; id={dataset.Id}");

                string datasetId = dataset.Id;

                // 列出已有的文档.
                RAGFlowSharp.Dtos.File.List.ResponseBody listResponseBody = await ragflowApi.ListFilesAsync(datasetId);

                if(listResponseBody.Code != 0)
                {
                    Console.WriteLine($"列出文档失败: {listResponseBody.Message}");
                    continue;
                }

                foreach (var document in listResponseBody.Data.Docs)
                {
                    Console.WriteLine($"文档: name={document.Name}; id={document.Id}; Type={document.Type}; ChunkCount={document.ChunkCount}");
                }
                Console.WriteLine();
            }


            // 列出聊天助手.
            var listChatAssistantResponse = await ragflowApi.ListChatAssistantsAsync();
            if (listChatAssistantResponse.Code != 0) 
            {
                Console.WriteLine($"获取聊天助手列表失败，返回结果 = {listChatAssistantResponse.Message}");
                return;
            }
            foreach(var chatAssistant in listChatAssistantResponse.Data)
            {
                Console.WriteLine($"聊天助手: name={chatAssistant.Name}; id={chatAssistant.Id}; Description={chatAssistant.Description}");


                var listSessionsAsyncResult = await ragflowApi.ListSessionsAsync(assistantId: chatAssistant.Id);
                if (listSessionsAsyncResult.Code != 0)
                {
                    Console.WriteLine($"获取会话列表失败，返回结果 = {listSessionsAsyncResult.Message}");
                    continue;
                }

                foreach(var session in listSessionsAsyncResult.Data)
                {
                    Console.WriteLine($"会话: name={session.Name}; id={session.Id}; AssistantId={session.AssistantId}");

                }
            }


        }



        /// <summary>
        /// 尝试测试一个全流程.
        /// 创建知识库.
        /// 上传文件.
        /// 解析文件.
        /// 创建聊天助理.
        /// 
        /// 结果是创建聊天助理的时候，失败了，因为文件没有完成解析。
        /// </summary>
        /// <param name="ragflowApi"></param>
        /// <returns></returns>
        static async Task TestAll(IRagflowApi ragflowApi)
        { 
            Console.WriteLine("--- 测试 RAGFlow 的全流程 ---");


            // 这里只填写一个知识库的名称.
            // 其它配置信息,全部使用默认值.
            RAGFlowSharp.Dtos.Dataset.Create.RequestBody createRequestBody = new RAGFlowSharp.Dtos.Dataset.Create.RequestBody()
            {
                Name = "C#创建的Dataset"
            };
            RAGFlowSharp.Dtos.Dataset.Create.ResponseBody createResponseBody = await ragflowApi.CreateDataset(createRequestBody);
            Console.WriteLine($"创建Dataset， Code = {createResponseBody.Code}");
            if(createResponseBody.Code != 0)
            {
                Console.WriteLine($"创建Dataset失败，错误信息 = {createResponseBody.Message}");
                return;
            }
            // 新创建的知识库ID.
            string datasetID = createResponseBody.Data.Id;
            Console.WriteLine($"新创建的知识库ID = {datasetID}");



            // 向知识库中添加文件.
            Console.WriteLine("开始向知识库中添加文件...");

            // 上传这个 pdf 文件，结果报错了，在管理页上手动上传，是正常的。
            // "DeepSeek从入门到精通.pdf" 这个文件，因为上传失败，所以已经从项目中删除了。
            // FileInfo uploadFileInfo = new FileInfo("DeepSeek从入门到精通.pdf");

            FileInfo uploadFileInfo = new FileInfo("test_rbac.sql");
            Upload.ResponseBody uploadResponseBody = await ragflowApi.UploadFilesAsync(datasetID, uploadFileInfo);
            Console.WriteLine($"上传文件， Code = {uploadResponseBody.Code}");
            if (uploadResponseBody.Code != 0)
            {
                Console.WriteLine($"上传文件失败，错误信息 = {uploadResponseBody.Message}");
                return;
            }
            // 文件ID.
            string fileID = uploadResponseBody.Data.FirstOrDefault().Id;
            Console.WriteLine($"新上传的文件ID = {fileID}");


            // 解析上传的文件.
            Console.WriteLine("开始解析文件...");
            Parse.RequestBody parseRequestBody = new Parse.RequestBody()
            {
                DocumentIds = new List<string>() { fileID }
            };
            Parse.ResponseBody parseResponseBody = await ragflowApi.ParseDocumentsAsync(datasetID, parseRequestBody);
            Console.WriteLine($"解析文件， Code = {parseResponseBody.Code}");
            if (parseResponseBody.Code != 0)
            {
                Console.WriteLine($"解析文件失败，错误信息 = {parseResponseBody.Message}");
                return;
            }




            // 注意：这里创建聊天助理的时候，报错了.
            // 错误信息：The dataset ... doesn't own parsed file
            // 也就是在前面上传的文件，还没有完成解析之前，这里没法创建聊天助理.

            // 创建一个聊天.
            RAGFlowSharp.Dtos.ChatAssistant.Create.RequestBody createAssistantRequestBody = new RAGFlowSharp.Dtos.ChatAssistant.Create.RequestBody()
            {
                Name = "C#创建的ChatAssistant",
                DatasetIds = new List<string>() { datasetID }
            };

            var createAssistantResponse = await ragflowApi.CreateChatAssistantAsync(createAssistantRequestBody);
            if (createAssistantResponse.Code != 0)
            {
                Console.WriteLine($"创建ChatAssistant失败，错误信息 = {createAssistantResponse.Message}");
                return;
            }

            RAGFlowSharp.Dtos.ChatAssistant.ChatAssistant  chatAssistant = createAssistantResponse.Data;

            Console.WriteLine($"创建ChatAssistant成功，返回结果 = {chatAssistant.Id}");

        }




        /// <summary>
        /// 创建聊天助理与会话.
        /// 
        /// 结果是查询会话，与创建会话的时候，返回 404 错误。
        /// </summary>
        /// <param name="ragflowApi"></param>
        /// <returns></returns>
        static async Task TestChatAssistant(IRagflowApi ragflowApi)
        {

            Console.WriteLine("开始测试ChatAssistant");

            var datasets = await ragflowApi.ListDatasets(name: "C#创建的Dataset");
            if(datasets.Code != 0)
            {
                Console.WriteLine($"获取Dataset列表失败，返回结果 = {datasets.Message}");
                return;
            }

            var dataset = datasets.Data.FirstOrDefault();
            if(dataset == null)
            {
                Console.WriteLine("未能检索到指定名称的知识库。");
                return;
            }

            string datasetID = dataset.Id;



            string chatAssistantID = null;


            var listChatAssistantsResult = await ragflowApi.ListChatAssistantsAsync(name: "C#创建的ChatAssistant");

            if(listChatAssistantsResult.Code == 0 && listChatAssistantsResult.Data.Length > 0){
                // 已存在有聊天助手， 不需要创建.
                chatAssistantID = listChatAssistantsResult.Data[0].Id;
                Console.WriteLine($"已经检索到聊天助理:{chatAssistantID}");
            } 
            else
            {
                // 创建一个聊天助理.
                RAGFlowSharp.Dtos.ChatAssistant.Create.RequestBody createAssistantRequestBody = new RAGFlowSharp.Dtos.ChatAssistant.Create.RequestBody()
                {
                    Name = "C#创建的ChatAssistant",
                    DatasetIds = new List<string>() { datasetID }
                };

                var createAssistantResponse = await ragflowApi.CreateChatAssistantAsync(createAssistantRequestBody);
                if (createAssistantResponse.Code != 0)
                {
                    Console.WriteLine($"创建ChatAssistant失败，错误信息 = {createAssistantResponse.Message}");
                    return;
                }

                RAGFlowSharp.Dtos.ChatAssistant.ChatAssistant chatAssistant = createAssistantResponse.Data;

                chatAssistantID = chatAssistant.Id;

                Console.WriteLine($"创建ChatAssistant成功，返回结果 = {chatAssistantID}");
            }



            string sessionID = null;

            var listSessionsAsyncResult = await ragflowApi.ListSessionsAsync(assistantId: chatAssistantID, name: "C#创建的会话");
            if (listSessionsAsyncResult.Code == 0 && listSessionsAsyncResult.Data.Length > 0)
            {
                // 已存在有会话。
                sessionID = listSessionsAsyncResult.Data[0].Id;

                Console.WriteLine($"已经检索到会话:{sessionID}");
            } 
            else
            {
                // 创建一个会话.
                RAGFlowSharp.Dtos.Session.Create.RequestBody createSessionRequestBody = new RAGFlowSharp.Dtos.Session.Create.RequestBody()
                {
                    Name = "C#创建的会话"
                };

                var createSessionResponse = await ragflowApi.CreateSessionAsync(chatAssistantID, createSessionRequestBody);

                if (createSessionResponse.Code != 0)
                {
                    Console.WriteLine($"创建会话失败，错误信息：{createSessionResponse.Message}");
                    return;
                }

                Session session = createSessionResponse.Data;
                sessionID = session.Id;
                Console.WriteLine($"创建会话成功，会话ID：{sessionID}");
            }

            


        }


    }
}

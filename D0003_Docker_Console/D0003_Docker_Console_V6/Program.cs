using System;
using System.IO;
using System.Threading.Tasks;


namespace D0003_Docker_Console_V6
{
    class Program
    {

        /// <summary>
        /// 例子代码。
        /// 参考页面：https://docs.microsoft.com/zh-cn/dotnet/core/docker/build-container?tabs=windows
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        static async Task Main(string[] args)
        {
            var counter = 0;
            var max = args.Length != 0 ? Convert.ToInt32(args[0]) : -1;


            string title = GetTitle();

            while (max == -1 || counter < max)
            {
                WriteCount($"{title}: {++counter} @ {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
                await Task.Delay(1000);
            }
        }






        #region 用于测试 Docker 中 volume 的使用 (配置文件).

        const string TITLE_FILE_NAME = @"./conf/title.txt";

        static string GetTitle()
        {
            if (!File.Exists(TITLE_FILE_NAME))
            {
                // 文件不存在.
                return "Error";
            }

            string result = File.ReadAllText(TITLE_FILE_NAME);
            return result.Trim();
        }

        #endregion





        #region 用于测试 Docker 中 volume 的使用 (输出文件).

        const string OUTPUT_FILE_NAME = @"./data/output.txt";

        static void WriteCount(string countInfo)
        {
            Console.WriteLine(countInfo);

            if (!Directory.Exists(@"./data"))
            {
                // 目录不存在.
                Directory.CreateDirectory(@"./data");
            }

            File.WriteAllText(OUTPUT_FILE_NAME, countInfo);
        }

        #endregion



    }
}

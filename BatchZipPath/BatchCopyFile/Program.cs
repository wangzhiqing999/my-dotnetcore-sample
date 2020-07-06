using System;
using System.IO;



namespace BatchCopyFile
{

    // 用途：
    // 在 BatchZipPath 项目， 运行完毕.
    // 手动删除掉 目录之后。
    // 当前目录下， 就只有压缩后的 zip 文件了。
    // 但是， 当单个目录下， 有 1000个文件的时候。
    // 对于阅读器来说， 就极其难受了.
    // 
    // 处理的办法，50个目录， 每个目录20个文件的节奏，进行处理.
    class Program
    {


        /// <summary>
        /// 每个目录的文件数量.
        /// </summary>
        private const int DIRECTORY_FILES_COUNT = 20;


        static void Main(string[] args)
        {
            // 获取 “当前目录”
            string currentPath = Directory.GetCurrentDirectory();
            Console.WriteLine($"Path: {currentPath}");

            DirectoryInfo di = new DirectoryInfo(Environment.CurrentDirectory);

            // 获取 当前目录 下， 所有的文件.
            FileInfo[] fi = di.GetFiles();


            // 文件索引.
            int fileIndex = 0;

            // 目录的索引.
            int directoryIndex = 0;

            // 当前子目录.
            string currentSubDirectory = "";

            foreach (FileInfo fileInfo in fi)
            {
                if(fileIndex == 0)
                {
                    directoryIndex++;
                    currentSubDirectory = directoryIndex.ToString();


                    Console.WriteLine($"CreateDirectory: {currentSubDirectory}");
                    Directory.CreateDirectory(currentSubDirectory);
                }

                string sourceFileName = $"{currentPath}\\{fileInfo.Name}";
                string toFileName = $"{currentPath}\\{currentSubDirectory}\\{fileInfo.Name}";

                Console.WriteLine($"Move {sourceFileName} to {toFileName}.");

                File.Move(sourceFileName, toFileName);

                // 文件数递增.
                fileIndex++;

                if(fileIndex > DIRECTORY_FILES_COUNT)
                {
                    // 超过一个目录的上限.
                    // 归零.
                    fileIndex = 0;
                }
            }


            Console.WriteLine("Finish.");
        }
    }
}

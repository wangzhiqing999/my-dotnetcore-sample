using System;
using System.IO;
using System.Linq;

namespace BatchRename
{


    // 用途：
    // 下载下来的漫画 zip 文件， 
    // 一个目录下， 有一千个文件.
    // 命名为
    // 1.[漫画名].zip
    // 2.[漫画名].zip
    // ......
    // 999.[漫画名].zip
    // 1000.[漫画名].zip
    // 
    // 预期想批量重命名为
    // 0001.[漫画名].zip
    // 0002.[漫画名].zip
    // ......
    // 0999.[漫画名].zip
    // 1000.[漫画名].zip
    // 这样的个格式.
    class Program
    {
        static void Main(string[] args)
        {
            // 获取 “当前目录”
            string currentPath = Directory.GetCurrentDirectory();
            Console.WriteLine($"Path: {currentPath}");

            DirectoryInfo di = new DirectoryInfo(Environment.CurrentDirectory);

            // 获取 当前目录 下， 所有的文件.
            FileInfo[] fi = di.GetFiles();


            foreach (FileInfo fileInfo in fi)
            {

                string fileName = fileInfo.Name;

                string[] fileParts = fileName.Split('.');
                if(fileParts.Length <= 1)
                {
                    // 忽略.
                    continue;
                }


                if(fileParts[0].Length >= 4)
                {
                    // 忽略文件代码长度已经大于等于4的.
                    continue;
                }


                int fileNo = 0;
                if(int.TryParse(fileParts[0], out fileNo) == false)
                {
                    // 解析文件名失败， 忽略.
                    continue;
                }

                


                fileParts[0] = fileNo.ToString("0000");

                string newFileName = string.Join('.', fileParts);



                string sourceFileName = $"{currentPath}\\{fileInfo.Name}";
                string toFileName = $"{currentPath}\\{newFileName}";

                Console.WriteLine($"Rename {fileInfo.Name} to {newFileName}");

                File.Move(sourceFileName, toFileName);
            }


            Console.WriteLine("Finish.");
        }
    }
}

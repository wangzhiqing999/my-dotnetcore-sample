using System;
using System.IO;
using System.IO.Compression;


namespace BatchZipPath
{

    // 用途：
    // 下载下来的漫画 zip 文件，
    // 一个大目录，下面很多的小目录。每个目录一本书.
    // 如果简单一个 大的 zip 文件，丢手机，导致翻页困难.
    // 如果简单大目录复制，由于小文件太多，复制时间太长。
    //
    // 处理的办法，将当前目录下，所有的目录，每个目录，创建一个 zip 压缩包
    // 这样，相当于 一个 zip 文件，一本书， 复制到手机上的时候， 时间也能接受， 翻页也没太大问题。
    class Program
    {
        static void Main(string[] args)
        {
            // 获取 “当前目录”
            string currentPath = Directory.GetCurrentDirectory();
            Console.WriteLine($"Path: {currentPath}");

            // 获取 当前目录 下， 所有的子目录.
            string[] subpaths = Directory.GetDirectories(currentPath);

            foreach(var path in subpaths)
            {
                string fileName = path.Replace(currentPath, "");
                string zipPath = $".{fileName}.zip";

                Console.WriteLine($"Create File: {zipPath}");
                ZipFile.CreateFromDirectory(path, zipPath);               
            }

            Console.WriteLine("Finish.");
        }
    }
}

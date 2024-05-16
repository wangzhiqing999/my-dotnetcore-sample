using System.IO;

namespace UpdateImageUpdateTime
{

    internal class Program
    {

        // 用途
        // 在 PC 上，整理图片， 按照 文件名 来排序。
        // 复制到 安卓手机上。
        // 手机相册默认排序是“修改时间逆序”的。
        // 相册可以手动修改排序方式。
        // 然而，某些 App，没有排序的功能。
        // 只好 PC 上，先按照 文件名来排序，然后依次修改 “修改时间”
        // 然后复制到 安卓手机上。
        // 这样，安卓手机上的 App, 打开相册时， 显示顺序是“修改时间逆序”的，与“按文件名称顺序”的结果一样。

        static void Main(string[] args)
        {

            Console.WriteLine("更新图片的修改时间！");

            if(args.Length == 0 )
            {
                Console.WriteLine("未指定目录！");
                return;
            }

            string path = args[0];
            if(!Directory.Exists(path))
            {
                Console.WriteLine($"目录“{path}”不存在！");
                return;
            }

            string[] files = Directory.GetFiles(path);
            List<string> filelist = files.OrderByDescending(f => f).ToList();

            DateTime startTime = DateTime.Today;

            for(int i = 0; i < filelist.Count; i++)
            {
                Console.WriteLine(filelist[i]);
                // 修改文件的修改时间
                File.SetLastWriteTime(filelist[i], startTime.AddMinutes(i));
            }

            Console.WriteLine("Finish!");
            
        }







    }
}

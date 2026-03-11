using System.Text;

namespace CutSmallLogFile
{
    internal class Program
    {


        // 用途
        // 
        static void Main(string[] args)
        {

            if (args.Length != 2)
            {
                Console.WriteLine("请指定日志文件名，与需要拆分的日期！");
                return;
            }


            string bigLogFile = args[0];
            if (!File.Exists(bigLogFile))
            {
                Console.WriteLine($"大日志文件“{bigLogFile}”不存在！");
                return;
            }

            string smallLogDateStr = args[1];
            if (!DateOnly.TryParse(smallLogDateStr, out DateOnly smallLogDate))
            {
                Console.WriteLine($"无法识别的日志日期：{smallLogDateStr}");
                return;
            }

            
            
            string outputFile = $"{bigLogFile}.{smallLogDate:yyyyMMdd}";

            StringBuilder outputBuff = new StringBuilder();


            // 开始处理标志.
            bool startFlag = false;


            string startDateStr = $"{smallLogDate:yyyy-MM-dd}";
            string finishDateStr = $"{smallLogDate.AddDays(1):yyyy-MM-dd}";


            using (var sr = new StreamReader(bigLogFile, Encoding.UTF8))
            {

                string? line = null;

                while(true)
                {
                    // 一次读取一行数据.
                    line = sr.ReadLine();

                    if(line == null)
                    {
                        break;
                    }


                    if (!startFlag)
                    {
                        // 尚未开始处理.
                        if (line.StartsWith(startDateStr))
                        {
                            // 开始.
                            startFlag = true;
                        }
                    } 
                    else
                    {
                        if (line.StartsWith(finishDateStr))
                        {
                            // 结束.
                            break;
                        }
                    }


                    if (startFlag)
                    {
                        outputBuff.AppendLine(line);
                    }

                    

                } 
            }

            File.WriteAllText(outputFile, outputBuff.ToString() );


            Console.WriteLine("Finish!");
        }
    }
}

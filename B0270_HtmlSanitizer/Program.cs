
// 引入Ganss.Xss命名空间，以便使用HtmlSanitizer类
using Ganss.Xss;


namespace B0270_HtmlSanitizer
{
    internal class Program
    {


        /// <summary>
        /// 测试的 HTML 字符串. 
        /// 包含一些不安全的代码和样式.
        /// </summary>
        private const string _html = @"<!DOCTYPE html>
<html lang=""en"">
<head>
    <meta charset=""UTF-8"" />
    <title>Document</title>
</head>
<body>
    <script>
        var x = 5;
        alert('test');
    </script>
    <style>
        body {
            background-color: #fff;
        }
    </style>
    <p>
        This is a paragraph. 

        <!-- 包含域名的全路径的链接地址. -->
        <a href=""http://www.google.com"" onclick=""alert('test')"">This is a link</a>.

        <!-- 相对路径的链接地址.  -->
        <a href=""/Test/User/123"" onclick=""alert('test')"">This is a test link</a>.

        <!-- 包含域名的全路径的图片. -->
        <img src=""http://www.google.com/logo.png"" onerror=""alert('test')"" />

        <!-- 相当路径图片. -->
        <img src=""logo.png"" onerror=""alert('test')"" />
    </p>
</body>
</html>";




        static void Main(string[] args)
        {
            // 创建HtmlSanitizer类的实例
            var sanitizer = new HtmlSanitizer();


            // 使用sanitizer对象的Sanitize方法来清理HTML
            // 第一个参数是要清理的HTML字符串
            // 第二个参数是基URL，用于解析相对URL
            var sanitized = sanitizer.Sanitize(_html, "https://www.xxx.com");


            Console.WriteLine(sanitized);
            Console.ReadLine();
        }


    }
}

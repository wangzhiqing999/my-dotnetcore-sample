using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Masuit.Tools;
using Masuit.Tools.Html;
using Masuit.Tools.Security;



namespace B0260_Masuit.Test
{
    internal class StringTest
    {

        public static void Test1()
        {
            Console.WriteLine("===== StringTest.Test1 =====");

            // 匹配IP地址
            string ipString = "114.114.114.114";
            bool isInetAddress = ipString.MatchInetAddress(); 
            Console.WriteLine($"{ipString} 是否是ip地址？ {isInetAddress}");

            // 匹配url地址
            string urlString = "https://www.baidu.com";
            bool isUrl = urlString.MatchUrl();
            Console.WriteLine($"{urlString} 是否是url地址？ {isUrl}");

            // 匹配手机号.
            string mobileString = "13888888888";
            bool isMobile = mobileString.MatchPhoneNumber();
            Console.WriteLine($"{mobileString} 是否是手机号？ {isMobile}");

            // 匹配身份证件号码.
            string idCardString = "123456789012345678";
            bool isIdCard = idCardString.MatchIdentifyCard();
            Console.WriteLine($"{idCardString} 是否是身份证件号码？ {isIdCard}");

        }


        public static void Test2()
        {
            Console.WriteLine("===== StringTest.Test2 =====");

            string html = @"<link href='/Content/font-awesome/css' rel='stylesheet'/>
        <!--[if IE 7]>
        <link href='/Content/font-awesome-ie7.min.css' rel='stylesheet'/>
        <![endif]-->
        <script src='/Scripts/modernizr'></script>
        <div id='searchBox' role='search'>
        <form action='/packages' method='get'>
        <span class='user-actions'><a href='/users/account/LogOff'>退出</a></span>
        <input name='q' id='searchBoxInput'/>
        <input id='searchBoxSubmit' type='submit' value='Submit' />
        </form>
        </div>";

            string s = html.HtmlSanitizerStandard();// 清理后：<div><span><a href="/users/account/LogOff">退出</a></span></div>

            Console.WriteLine($"清理后：{s}");

            // string s2 = html.HtmlSanitizerCustom(); // 自定义清理
            // Console.WriteLine($"清理后：{s2}");

        }






        public static void Test3()
        {
            Console.WriteLine("===== StringTest.Test3 =====");
         
            
            string str = "13012345678";

            Console.WriteLine($"{str} 的 MD5 = {str.MDString()}");

            Console.WriteLine($"{str} 的 SHA256 = {str.SHA256()}");


        }



        public static void Test4()
        {
            Console.WriteLine("===== StringTest.Test4 =====");

            string phone = "13123456789";
            Console.WriteLine($"手机号 {phone} 的 Mask = {phone.Mask()}");

            string email = "abc@163.com";
            Console.WriteLine($"{email} 的 MaskEmail = {email.MaskEmail()}");


        }



    }
}

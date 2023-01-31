using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;


namespace P0002_MyGrid.Jobs.Util
{
    class MailSender
    {

        /// <summary>
        /// 向用户发送邮件
        /// </summary>
        /// <param name="ReceiveUser">接收人，邮箱地址</param>
        /// <param name="ReceiveName">接收人，显示的名称</param>
        /// <param name="SendUser">发件人，邮箱地址</param>
        /// <param name="DisplayName">发件人，显示的名称</param>
        /// <param name="SendUserName">发件人，邮箱地址</param>
        /// <param name="UserPassword">发件人，邮箱密码</param>
        /// <param name="MailTitle">邮件标题</param>
        /// <param name="MailContent">邮件内容</param>
        private static void SendMail(string ReceiveUser, string ReceiveName, string SendUser, string DisplayName, string SendUserName, string UserPassword, string MailTitle, string MailContent)
        {
            MailAddress toMail = new MailAddress(ReceiveUser, ReceiveName);//接收者邮箱
            MailAddress fromMail = new MailAddress(SendUser, DisplayName);//发送者邮箱
            MailMessage mail = new MailMessage(fromMail, toMail);
           
            mail.Subject = MailTitle;
            mail.IsBodyHtml = true;//是否支持HTML
            mail.Body = MailContent;


            SmtpClient client = new SmtpClient();
            client.EnableSsl = true;
            client.Host = "smtp.exmail.qq.com";//设置发送者邮箱对应的smtpserver
            client.Port = 587;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(SendUserName, UserPassword);
            client.DeliveryMethod = SmtpDeliveryMethod.Network;

            client.Send(mail);
        }







        public static void SendMail(string MailTitle, string MailContent)
        {

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("./config/app.json", false)
                .Build();

            //接收者邮箱
            string receiveUser = configuration.GetSection("MailReceiveUser").Value; ;                
            string receiveName = configuration.GetSection("MailReceiveName").Value; ;
                
            //发送者邮箱
            string sendUser = configuration.GetSection("MailSendUser").Value;
            string sendName = configuration.GetSection("MailSendName").Value;

            // 用户名与密码.
            string sendUserName = configuration.GetSection("MailSendUserName").Value;
            string userPassword = configuration.GetSection("MailUserPassword").Value; ;


            SendMail(
                ReceiveUser: receiveUser,
                ReceiveName: receiveName,
                SendUser: sendUser,
                DisplayName: sendName,
                SendUserName: sendUserName,
                UserPassword: userPassword,
                MailTitle: MailTitle,
                MailContent: MailContent
                );

        }



    }
}

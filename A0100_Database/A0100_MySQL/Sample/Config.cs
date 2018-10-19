using System;
using System.Collections.Generic;
using System.Text;

using MySql.Data;
using MySql.Data.MySqlClient;


namespace A0100_MySQL.Sample
{
    class Config
    {
        /// <summary>
        /// 注意：如果连接过程，报了 The host 192.168.1.15 does not support SSL connections
        /// 解决办法是在连接字符串中， 增加 SslMode=none 的设置。
        /// </summary>
        public const string ConnString = @"Server=192.168.1.15;Database=test;Uid=test;Pwd=123456;CharSet=utf8;SslMode=none";


    }
}

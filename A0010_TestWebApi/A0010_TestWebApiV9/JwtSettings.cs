﻿namespace A0010_TestWebApiV9
{
    public class JwtSettings
    {
        /// <summary>
        /// token是谁颁发的
        /// </summary>
        public string Issuer { get; set; }

        /// <summary>
        /// token可以给哪些客户端使用
        /// </summary>
        public string Audience { get; set; }

        /// <summary>
        /// 加密的key
        /// </summary>
        public string SecretKey { get; set; }
    }
}

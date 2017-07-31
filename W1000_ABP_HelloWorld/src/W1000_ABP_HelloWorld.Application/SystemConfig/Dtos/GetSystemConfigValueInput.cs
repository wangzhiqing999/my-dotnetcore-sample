using System;
using System.Collections.Generic;
using System.Text;

namespace W1000_ABP_HelloWorld.SystemConfig.Dtos
{
    public class GetSystemConfigValueInput
    {

        /// <summary>
        /// 配置类型代码.
        /// </summary>
        public string ConfigTypeCode { set; get; }


        /// <summary>
        /// 配置代码.
        /// </summary>
        public string ConfigCode { set; get; }

    }
}

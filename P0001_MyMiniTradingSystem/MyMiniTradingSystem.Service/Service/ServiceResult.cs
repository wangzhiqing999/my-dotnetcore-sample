using System;
using System.Collections.Generic;
using System.Text;

namespace MyMiniTradingSystem.Service
{

    /// <summary>
    /// 服务结果.
    /// </summary>
    public class ServiceResult
    {

        public ServiceResult()
        {
        }

        public ServiceResult(int code, string msg)
        {
            this.ResultCode = code;
            this.ResultMessage = msg;
        }



        /// <summary>
        /// 默认的 成功的服务结果.
        /// </summary>
        public static readonly ServiceResult SuccessServiceResult = new ServiceResult(0, "SUCCESS");


        /// <summary>
        /// 结果代码.
        /// </summary>
        public int ResultCode { set; get; }


        /// <summary>
        /// 结果消息.
        /// </summary>
        public string ResultMessage { get; set; }



        public override string ToString()
        {
            return $"ServiceResult[ Code = {this.ResultCode}; Message = {this.ResultMessage}]";
        }

    }


}

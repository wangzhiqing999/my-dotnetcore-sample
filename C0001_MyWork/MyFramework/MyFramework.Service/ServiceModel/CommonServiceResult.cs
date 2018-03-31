using System;
using System.Text;

namespace MyFramework.ServiceModel
{
    public class CommonServiceResult
    {

        public CommonServiceResult()
        {
        }

        public CommonServiceResult(Exception ex)
        {
            this.ResultCode = -99999;
            this.ResultMessage = ex.Message;
        }


        /// <summary>
        /// 结果码.
        /// </summary>
        public int ResultCode { set; get; }


        /// <summary>
        /// 结果消息.
        /// </summary>
        public string ResultMessage { set; get; }


        /// <summary>
        /// 结果数据.
        /// </summary>
        public dynamic ResultData { set; get; }



        /// <summary>
        /// 是否执行成功.
        /// </summary>
        public bool IsSuccess
        {
            get
            {
                return this.ResultCode == ResultCodeIsSuccess;
            }
        }


        public override string ToString()
        {
            StringBuilder buff = new StringBuilder();
            buff.AppendLine(this.ResultCode.ToString());
            buff.AppendLine(this.ResultMessage);
            return buff.ToString();
        }


        /// <summary>
        /// 处理成功.
        /// </summary>
        public const int ResultCodeIsSuccess = 0;

        /// <summary>
        /// 默认的，成功的执行结果.
        /// </summary>
        public static readonly CommonServiceResult DefaultSuccessResult = new CommonServiceResult()
        {
            ResultCode = ResultCodeIsSuccess,
            ResultMessage = "success",
        };


    }
}

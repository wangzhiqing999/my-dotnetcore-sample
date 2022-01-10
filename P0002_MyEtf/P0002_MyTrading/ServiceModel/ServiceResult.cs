using System;

namespace P0002_MyTrading.ServiceModel
{
    public class ServiceResult
    {

        public ServiceResult(string code, string message)
        {
            this.ResultCode = code;
            this.ResultMessage = message;
        }

        public ServiceResult(Exception ex)
        {
            this.ResultCode = "SYSTEM_EXCEPTION";
            this.ResultMessage = ex.Message;
        }



        /// <summary>
        /// 结果码.
        /// </summary>
        public string ResultCode { set; get; }


        /// <summary>
        /// 结果消息.
        /// </summary>
        public string ResultMessage { set; get; }


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
            return $"ResultCode={ResultCode}; ResultMessage={ResultMessage}";
        }



        /// <summary>
        /// 处理成功.
        /// </summary>
        public const string ResultCodeIsSuccess = "0";

        /// <summary>
        /// 默认的，成功的执行结果.
        /// </summary>
        public static readonly ServiceResult DefaultSuccessResult = new ServiceResult(ResultCodeIsSuccess,"success");




        /// <summary>
        /// 数据不存在.
        /// </summary>
        public const string ResultCodeIsDataNotFound = "COMMON_DATA_NOT_FOUND";

        /// <summary>
        /// 默认的，数据不存在的执行结果.
        /// </summary>
        public static readonly ServiceResult DataNotFoundResult = new ServiceResult(ResultCodeIsDataNotFound, "数据不存在！");




        /// <summary>
        /// 数据已存在.
        /// </summary>
        public const string ResultCodeIsDataHadExists = "COMMON_DATA_HAD_EXISTS";

        /// <summary>
        /// 默认的，数据已存在的执行结果.
        /// </summary>
        public static readonly ServiceResult DataHadExistsResult = new ServiceResult(ResultCodeIsDataHadExists,"数据已存在！");


    }
}

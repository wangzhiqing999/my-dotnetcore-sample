using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace P0002_MyGrid.ServiceModel
{


    /// <summary>
    /// 获取待办事项的请求.
    /// </summary>
    public class GetTodoRequest
    {
        /// <summary>
        /// 项目代码
        /// </summary>
        [DataMember]
        public string ItemCode { set; get; }


        /// <summary>
        /// 当前价格.
        /// </summary>
        [DataMember]
        public decimal CurrentPrice { set; get; }
    }



}

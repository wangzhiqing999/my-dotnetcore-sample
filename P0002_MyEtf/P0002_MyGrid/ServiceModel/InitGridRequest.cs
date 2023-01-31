using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.Serialization;


namespace P0002_MyGrid.ServiceModel
{


    /// <summary>
    /// 初始化网格请求.
    /// </summary>
    public class InitGridRequest
    {


        /// <summary>
        /// 项目代码
        /// </summary>
        [DataMember]
        public string ItemCode { set; get; }



        /// <summary>
        /// 最高价格.
        /// </summary>
        [DataMember]
        public decimal MaxPrice { set; get; }



        /// <summary>
        /// 最低价格.
        /// </summary>
        [DataMember]
        public decimal MinPrice { set; get; }



        /// <summary>
        /// 网格数.
        /// </summary>
        [DataMember]
        public int NumOfGrid { set; get; } = 10;


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace P0002_MyGrid.ServiceModel
{


    /// <summary>
    /// 做什么操作的枚举.
    /// </summary>
    public enum DoWhat
    {
        /// <summary>
        /// 无操作.
        /// </summary>
        None = 0,

        /// <summary>
        /// 买入.
        /// </summary>
        Buy = 1,


        /// <summary>
        /// 卖出.
        /// </summary>
        Sell = 2,
    }




    /// <summary>
    /// 获取待办事项的应答.
    /// </summary>
    public class GetTodoResponse
    {

        /// <summary>
        /// 项目代码
        /// </summary>
        [DataMember]
        public string ItemCode { set; get; }



        /// <summary>
        /// 网格代码
        /// </summary>
        [DataMember]
        public int GridNo { set; get; }




        /// <summary>
        /// 操作.
        /// </summary>
        [DataMember]
        public DoWhat Todo { get; set; }





        /// <summary>
        /// 挂单价格.
        /// </summary>
        [DataMember]
        public decimal Price { set; get; }



        /// <summary>
        /// 挂单数量.
        /// <br/>
        /// 操作那里定义了 买入，还是卖出。
        /// 这里只存储一个 正整数。
        /// </summary>
        [DataMember]
        public int Quantity { set; get; }



        public override string ToString()
        {

            switch (this.Todo)
            {

                case DoWhat.Buy:
                    return $"买入 {this.ItemCode}，网格：{GridNo}， 价格：{this.Price} 数量：{this.Quantity} ";

                case DoWhat.Sell:
                    return $"卖出 {this.ItemCode}，网格：{GridNo}， 价格：{this.Price} 数量：{this.Quantity} ";

                default:
                    return "无操作！";
            }

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using System.Runtime.Serialization;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace P0002_MyEtf.Model
{

    [Keyless]
    public class EtfMaData
    {


        /// <summary>
        /// ETF代码
        /// </summary>
        [DataMember]
        [Column("etf_code")]
        public string EtfCode { set; get; }


        /// <summary>
        /// 交易日期
        /// </summary>
        [DataMember]
        [Column("trading_date")]
        public DateTime TradingDate { set; get; }


        /// <summary>
        /// MA值.
        /// </summary>
        [DataMember]
        [Column("ma")]
        public decimal MaValue { set; get; }


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Postgrest;
using Postgrest.Attributes;
using Postgrest.Models;


namespace TestPostgrest.Model
{

    [Table("etf_master")]
    public class EtfMaster : BaseModel
    {

        /// <summary>
        /// ETF代码
        /// </summary>
        [PrimaryKey("etf_code")]
        public string EtfCode { set; get; }


        /// <summary>
        /// ETF名称
        /// </summary>
        [Column("etf_name")]
        public string EtfName { set; get; }




        public override string ToString()
        {
            return $"{EtfName}({EtfCode})";
        }


    }
}

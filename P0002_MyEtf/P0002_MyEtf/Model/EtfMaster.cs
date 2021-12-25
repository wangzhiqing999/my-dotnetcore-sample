using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.Serialization;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace P0002_MyEtf.Model
{

    /// <summary>
    /// ETF主数据.
    /// </summary>
    [Table("etf_master", Schema = "my_etf")]
    public class EtfMaster
    {

        /// <summary>
        /// ETF代码
        /// </summary>
        [DataMember]
        [Column("etf_code")]
        [Display(Name = "ETF代码")]
        [StringLength(16)]
        [Key]
        public string EtfCode { set; get; }


        /// <summary>
        /// ETF名称
        /// </summary>
        [DataMember]
        [Column("etf_name")]
        [Display(Name = "ETF名称")]
        [StringLength(64)]
        public string EtfName { set; get; }






        #region 一对多.


        /// <summary>
        /// ETF日线列表.
        /// </summary>
        public List<EtfDayLine> EtfDayLineList { set; get; }


        /// <summary>
        /// ETF日波幅列表.
        /// </summary>
        public List<EtfDayTr> EtfDayTrList { set; get; }




    


        /// <summary>
        /// ETF周线列表.
        /// </summary>
        public List<EtfWeekLine> EtfWeekLineList { set; get; }



        #endregion


    }
}

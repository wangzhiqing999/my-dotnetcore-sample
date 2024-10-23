using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A0100_MySQL.DapperAccessEF
{
    internal class DocumentType
    {
        /// <summary>
        /// 文档类型代码.
        /// </summary>
        public string DocumentTypeCode { set; get; }

        /// <summary>
        /// 文档类型名称.
        /// </summary>
        public string DocumentTypeName { set; get; }



        public override string ToString()
        {
            return $"{DocumentTypeCode}:{DocumentTypeName}";
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using Npgsql;

using P0002_MyEtf.DataAccess;
using P0002_MyEtf.Model;
using P0002_MyEtf.Service;
using P0002_MyEtf.ServiceModel;



namespace P0002_MyEtf.ServiceImpl
{
    public partial class DefaultEtfDayService : IEtfDayService
    {




        /// <summary>
        /// 获取日线 MA 的SQL语句.
        /// <br/>
        /// 注意：
        /// EntityFrameworkCore 与 EntityFramework 的写法不一样.
        /// 
        /// 这里是创建一个视图，来定义模型
        /// 参考：
        /// https://docs.microsoft.com/zh-cn/ef/core/modeling/keyless-entity-types?tabs=data-annotations
        /// 
        /// 
        /// https://docs.microsoft.com/zh-cn/ef/core/querying/raw-sql
        /// 
        /// </summary>
        private const string GetEtfDayMaSql = @"
SELECT 
    :etfCode AS etf_code,
    trading_date,
    ma
FROM
    my_etf.get_day_ma(:etfCode, :maNum)
";




        /// <summary>
        /// 获取日线的 MA.
        /// <br/>
        /// 这个是通过调用 Postgres 下面写的函数来实现的.
        /// </summary>
        /// <param name="etfCode"></param>
        /// <param name="maNum"></param>
        /// <returns></returns>
        public List<EtfMaData> GetEtfDayMa(string etfCode, int maNum)
        {

            NpgsqlParameter etfCodeParam = new NpgsqlParameter(":etfCode", etfCode);
            NpgsqlParameter maNumParam = new NpgsqlParameter(":maNum", maNum);


            // 注意：
            // 这个 FromSqlRaw 方法， 程序集: Microsoft.EntityFrameworkCore.Relational.dll
            // 需要可执行程序的项目， 再 NuGet 添加一个引用 Microsoft.EntityFrameworkCore.Relational v5.0.x
            List<EtfMaData> resultList = this._MyEtfContext.EtfMaDatas.FromSqlRaw(GetEtfDayMaSql, etfCodeParam, maNumParam).ToList();

            return resultList;

        }

    }
}

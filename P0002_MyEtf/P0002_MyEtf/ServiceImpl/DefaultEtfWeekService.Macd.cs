using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using Npgsql;


using P0002_MyEtf.Model;
using P0002_MyEtf.Service;



namespace P0002_MyEtf.ServiceImpl
{
    public partial class DefaultEtfWeekService : IEtfWeekService
    {



        /// <summary>
        /// 获取 MACD 周线的SQL语句.
        /// <br/>
        /// 注意：
        /// EntityFrameworkCore 与 EntityFramework 的写法不一样.
        /// </summary>
        private const string GetEtfWeekMacdSql = @"
SELECT 
    :etfCode AS etf_code,
    trading_date,
    diff,
    dea,
    macd
FROM
    my_etf.get_week_macd(:etfCode)
";



        public List<EtfWeekMacd> GetEtfWeekMacd(string etfCode)
        {
            NpgsqlParameter etfCodeParam = new NpgsqlParameter(":etfCode", etfCode);

            // 注意：
            // 这个 FromSqlRaw 方法， 程序集: Microsoft.EntityFrameworkCore.Relational.dll
            // 需要可执行程序的项目， 再 NuGet 添加一个引用 Microsoft.EntityFrameworkCore.Relational v5.0.x
            List<EtfWeekMacd> resultList = this._MyEtfContext.EtfWeekMacds.FromSqlRaw(GetEtfWeekMacdSql, etfCodeParam).AsNoTracking().ToList();

            return resultList;
        }

    }
}

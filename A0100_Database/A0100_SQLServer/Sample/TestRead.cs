using System;
using System.Collections.Generic;
using System.Text;

using System.Data;
using System.Data.SqlClient;


namespace A0100_SQLServer.Sample
{
    class TestRead
    {
        
        private const string Sql = @"SELECT id, a, b, c FROM test_abc";


        // 测试读取 SQL Server 的数据.
        // 代码写法， 与类库的命名空间  与 .Net Framework 的没啥区别,
        // 不同的地方， 是 需要手动 nuget 引用 System.Data.Common 与 System.Data.SqlClient
        public static void DoTest()
        {
            Console.WriteLine("Test Read SQL Server Data...");

            using (SqlConnection conn = new SqlConnection(Config.ConnString))
            {
                conn.Open();

                using(SqlCommand cmd = new SqlCommand (Sql, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine("id = {0}; a = {1}; b = {2}; c = {3}", reader["id"], reader["a"], reader["b"], reader["c"]);
                        }
                    }
                }
            }
        }


    }
}

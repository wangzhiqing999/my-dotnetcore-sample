using System;
using System.Collections.Generic;
using System.Text;

using MySql.Data;
using MySql.Data.MySqlClient;


namespace A0100_MySQL.Sample
{
    class TestRead
    {


        private const string Sql = @"SELECT id, a, b, c FROM test_abc";


        // 测试读取 MySQL 的数据.
        // 需要手动 nuget 引用 MySql.Data
        public static void DoTest()
        {
            Console.WriteLine("Test Read MySql Data...");

            using (MySqlConnection conn = new MySqlConnection(Config.ConnString))
            {
                conn.Open();

                using(MySqlCommand cmd = new MySqlCommand(Sql, conn))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine("id = {0}; a = {1}; b = {2}", reader["id"], reader["a"], reader["b"]);
                        }
                    }                        
                }
            }
        }


    }
}

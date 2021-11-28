using System;
using System.Collections.Generic;
using System.Text;


using Microsoft.Data.Sqlite;


namespace A0100_SQLite.Sample
{
    class TestRead
    {


        private const string Sql = @"SELECT id, a, b, c FROM test_abc";


        // 测试读取 SQLite 的数据.
        public static void DoTest()
        {
            Console.WriteLine("Test Read SQLite Data...");

            using (SqliteConnection conn = new SqliteConnection(Config.ConnString))
            {
                conn.Open();

                using(SqliteCommand cmd = new SqliteCommand(Sql, conn))
                {
                    using (SqliteDataReader reader = cmd.ExecuteReader())
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

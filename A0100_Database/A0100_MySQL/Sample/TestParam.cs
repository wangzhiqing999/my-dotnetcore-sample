using System;
using System.Collections.Generic;
using System.Text;

using MySql.Data;
using MySql.Data.MySqlClient;


namespace A0100_MySQL.Sample
{
    class TestParam
    {

        private const string SelectSql = @"SELECT id, a, b, c FROM test_abc WHERE id = ?id";
        private const string UpdateSql = @"UPDATE test_abc SET a = a + 1, b = b + 2, c = c + 3 WHERE id = ?id";



        public static void DoTest(string id)
        {
            Console.WriteLine("Test Read/Write MySql Data... id = {0} ", id);

            using (MySqlConnection conn = new MySqlConnection(Config.ConnString))
            {
                conn.Open();

                using (MySqlCommand cmd = new MySqlCommand(SelectSql, conn))
                {
                    MySqlParameter param = new MySqlParameter("id", id);
                    cmd.Parameters.Add(param);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine("id = {0}; a = {1}; b = {2}; c = {3}", reader["id"], reader["a"], reader["b"], reader["c"]);
                        }
                    }                        
                }



                using (MySqlCommand cmd2 = new MySqlCommand(UpdateSql, conn))
                {
                    MySqlParameter param = new MySqlParameter("id", id);
                    cmd2.Parameters.Add(param);

                    int rowCount = cmd2.ExecuteNonQuery();
                    Console.WriteLine("Update Rows : {0}", rowCount);
                }
            }
        }

    }
}

using System;
using System.Collections.Generic;
using System.Text;

using MySql.Data;
using MySql.Data.MySqlClient;


namespace A0100_MySQL.Sample
{
    class TestWrite
    {
        
        private const string Sql = @"UPDATE test_abc SET a = a + 1, b = b + 2, c = c + 3";


        public static void DoTest()
        {
            Console.WriteLine("Test Write MySql Data...");

            using (MySqlConnection conn = new MySqlConnection(Config.ConnString))
            {
                conn.Open();

                using (MySqlCommand cmd = new MySqlCommand(Sql, conn))
                {
                    int rowCount = cmd.ExecuteNonQuery();
                    Console.WriteLine("Update Rows : {0}", rowCount);
                }
            }
        }


    }
}

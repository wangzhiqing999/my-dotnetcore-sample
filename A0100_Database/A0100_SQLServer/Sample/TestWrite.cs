using System;
using System.Collections.Generic;
using System.Text;

using System.Data;
using System.Data.SqlClient;


namespace A0100_SQLServer.Sample
{
    class TestWrite
    {
        
        private const string Sql = @"UPDATE test_abc SET a = a + 1, b = b + 2, c = c + 3";


        public static void DoTest()
        {
            Console.WriteLine("Test Write SQL Server Data...");

            using (SqlConnection conn = new SqlConnection(Config.ConnString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(Sql, conn))
                {
                    int rowCount = cmd.ExecuteNonQuery();
                    Console.WriteLine("Update Rows : {0}", rowCount);
                }
            }
        }


    }
}

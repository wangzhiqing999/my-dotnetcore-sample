using System;
using System.Collections.Generic;
using System.Text;


using Microsoft.Data.Sqlite;


namespace A0100_SQLite.Sample
{
    class TestWrite
    {
        
        private const string Sql = @"UPDATE test_abc SET a = a + 1, b = b + 2, c = c + 3";


        public static void DoTest()
        {
            Console.WriteLine("Test Write SQLite Data...");

            using (SqliteConnection conn = new SqliteConnection(Config.ConnString))
            {
                conn.Open();

                using (SqliteCommand cmd = new SqliteCommand(Sql, conn))
                {
                    int rowCount = cmd.ExecuteNonQuery();
                    Console.WriteLine("Update Rows : {0}", rowCount);
                }
            }
        }


    }
}

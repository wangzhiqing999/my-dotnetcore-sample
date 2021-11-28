using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Data.Sqlite;


namespace A0100_SQLite.Sample
{
    class TestParam
    {

        private const string SelectSql = @"SELECT id, a, b, c FROM test_abc WHERE id = @id";

        private const string SelectLikeSql = @"SELECT id, a, b, c FROM test_abc WHERE id LIKE @id";

        private const string UpdateSql = @"UPDATE test_abc SET a = a + 1, b = b + 2, c = c + 3 WHERE id = @id";



        public static void DoTest(string id)
        {
            Console.WriteLine("Test Read/Write Sqlite Data... id = {0} ", id);

            using (SqliteConnection conn = new SqliteConnection(Config.ConnString))
            {
                conn.Open();


                Console.WriteLine("----- Select ----- ");

                using (SqliteCommand cmd = new SqliteCommand(SelectSql, conn))
                {
                    SqliteParameter param = new SqliteParameter("@id", id);
                    cmd.Parameters.Add(param);

                    using (SqliteDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine("id = {0}; a = {1}; b = {2}; c = {3}", reader["id"], reader["a"], reader["b"], reader["c"]);
                        }
                    }
                }


                Console.WriteLine("----- Select ----- Where Like ----- ");

                using (SqliteCommand likeCmd = new SqliteCommand(SelectLikeSql, conn))
                {
                    SqliteParameter param = new SqliteParameter("@id", id + "%");
                    likeCmd.Parameters.Add(param);

                    using (SqliteDataReader reader = likeCmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine("id = {0}; a = {1}; b = {2}; c = {3}", reader["id"], reader["a"], reader["b"], reader["c"]);
                        }
                    }
                }


                Console.WriteLine("----- Update ----- ");

                using (SqliteCommand cmd2 = new SqliteCommand(UpdateSql, conn))
                {
                    SqliteParameter param = new SqliteParameter("@id", id);
                    cmd2.Parameters.Add(param);

                    int rowCount = cmd2.ExecuteNonQuery();
                    Console.WriteLine("Update Rows : {0}", rowCount);
                }
            }
        }

    }
}

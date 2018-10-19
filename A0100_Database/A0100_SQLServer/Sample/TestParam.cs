using System;
using System.Collections.Generic;
using System.Text;

using System.Data;
using System.Data.SqlClient;


namespace A0100_SQLServer.Sample
{
    class TestParam
    {

        private const string SelectSql = @"SELECT id, a, b, c FROM test_abc WHERE id = @id";
        private const string UpdateSql = @"UPDATE test_abc SET a = a + 1, b = b + 2, c = c + 3 WHERE id = @id";



        public static void DoTest(string id)
        {
            Console.WriteLine("Test Read/Write SQL Server Data... id = {0} ", id);

            using (SqlConnection conn = new SqlConnection(Config.ConnString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(SelectSql, conn))
                {
                    SqlParameter param = new SqlParameter("id", id);
                    cmd.Parameters.Add(param);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine("id = {0}; a = {1}; b = {2}; c = {3}", reader["id"], reader["a"], reader["b"], reader["c"]);
                        }
                    }                        
                }



                using (SqlCommand cmd2 = new SqlCommand(UpdateSql, conn))
                {
                    SqlParameter param = new SqlParameter("id", id);
                    cmd2.Parameters.Add(param);

                    int rowCount = cmd2.ExecuteNonQuery();
                    Console.WriteLine("Update Rows : {0}", rowCount);
                }
            }
        }

    }
}

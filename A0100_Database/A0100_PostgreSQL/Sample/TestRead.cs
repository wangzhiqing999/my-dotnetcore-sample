using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Npgsql;


namespace A0100_PostgreSQL.Sample
{
    internal class TestRead
    {

        private const string Sql = @"SELECT id, a, b, c FROM test_abc";



        public static void DoTest()
        {
            Console.WriteLine("Test Read PostgreSQL Data...");

            using (NpgsqlConnection conn = new NpgsqlConnection(Config.ConnString))
            {
                conn.Open();

                using (NpgsqlCommand cmd = new NpgsqlCommand(Sql, conn))
                {
                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
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

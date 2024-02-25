using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MySql.Data.MySqlClient;
using Dapper;

using A0100_MySQL.Sample;


namespace A0100_MySQL.DapperSample
{

    /// <summary>
    /// 使用 Dapper 来访问 MySql 的例子代码.
    /// </summary>
    internal class TestDapper
    {



        private const string SelectAllSql = @"SELECT id, a, b, c FROM test_abc";


        private const string SelectOneSql = @"SELECT id, a, b, c FROM test_abc WHERE id = 'TEST_DAPPER'";


        private const string InsertSql = @"INSERT INTO test_abc(id, a, b, c) VALUES (?Id, ?A, ?B, ?C)";


        private const string UpdateSql = @"UPDATE 
    test_abc
SET
    a = ?A, 
    b = ?B, 
    c = ?C
WHERE
    id = ?Id";


        private const string DeleteSql = @"DELETE FROM test_abc WHERE id = ?Id";


        public static void DoTestSelect()
        {
            Console.WriteLine("Test Select MySql Data...");

            using (MySqlConnection conn = new MySqlConnection(Config.ConnString))
            {
                conn.Open();


                Console.WriteLine("--- select all ---");
                var testAbcs = conn.Query<TestAbc>(SelectAllSql);
                foreach(var test in testAbcs)
                {
                    Console.WriteLine(test);
                }


                Console.WriteLine("--- select one ---");
                var testAbc = conn.QueryFirstOrDefault<TestAbc>(SelectOneSql);
                Console.WriteLine(testAbc);

            }
        }



        public static void DoTestInsert()
        {
            Console.WriteLine("Test Insert MySql Data...");


            using (MySqlConnection conn = new MySqlConnection(Config.ConnString))
            {
                conn.Open();


                conn.Execute(InsertSql,
                    new TestAbc()
                    {
                        Id = "TEST_DAPPER",
                        A = 100,
                        B = 200,
                        C = 300
                    });
            }
        }


        public static void DoTestUpdate()
        {
            Console.WriteLine("Test Update MySql Data...");


            using (MySqlConnection conn = new MySqlConnection(Config.ConnString))
            {
                conn.Open();


                conn.Execute(UpdateSql,
                    new TestAbc()
                    {
                        Id = "TEST_DAPPER",
                        A = 400,
                        B = 500,
                        C = 600
                    });
            }
        }




        public static void DoTestDelete()
        {
            Console.WriteLine("Test Delete MySql Data...");


            using (MySqlConnection conn = new MySqlConnection(Config.ConnString))
            {
                conn.Open();


                conn.Execute(DeleteSql,
                    new 
                    {
                        Id = "TEST_DAPPER"                        
                    });
            }
        }



    }
}

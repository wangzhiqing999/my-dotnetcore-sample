using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;

using Microsoft.Data.Sqlite;


namespace A0100_SQLite.Sample
{
    class TestInit
    {




        private const string CREATE_TABLE_SQL = @"CREATE TABLE test_abc (
	id	VARCHAR(32)  PRIMARY KEY,
	a	INT,
	b	INT,
	c	INT
)";


        private const string TEST_DATA_SQL1 = @"INSERT INTO test_abc(
	id, a, b, c
) VALUES (
	'TEST', 1,2,3
)";


        private const string TEST_DATA_SQL2 = @"INSERT INTO test_abc(
	id, a, b, c
) VALUES (
	'TEST2', 4,5,6
)";



        public static void DoInit()
        {

            if (!File.Exists("A0100_SQLite.db"))
            {

                // 文件不存在， 意味着数据库还没有.
                // 创建库之后， 需要先执行建表语句.


                using(var conn= new SqliteConnection( Config.ConnString))
                {
                    conn.Open();



                    using(var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = CREATE_TABLE_SQL;
                        cmd.ExecuteNonQuery();

                        cmd.CommandText = TEST_DATA_SQL1;
                        cmd.ExecuteNonQuery();

                        cmd.CommandText = TEST_DATA_SQL2;
                        cmd.ExecuteNonQuery();
                    }


                }





            }


        }


    }
}

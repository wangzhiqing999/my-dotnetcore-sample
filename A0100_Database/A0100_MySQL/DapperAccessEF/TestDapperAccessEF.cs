using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Dapper;

using A0100_MySQL.Sample;
using MySql.Data.MySqlClient;




namespace A0100_MySQL.DapperAccessEF
{

    /// <summary>
    /// 这里是测试，之前 A0005_EF_Mysql_V6 项目，使用 EntityFrameworkCore 生成的表。
    /// 然后，用 Dapper 来访问的例子.
    /// </summary>
    internal class TestDapperAccessEF
    {

        private const string SelectAllSql = @"SELECT 
    document_type_code AS DocumentTypeCode, 
    document_type_name AS DocumentTypeName
FROM 
    document_type";


        private const string SelectOneSql = @"SELECT 
    document_type_code AS DocumentTypeCode, 
    document_type_name AS DocumentTypeName 
FROM 
    document_type 
WHERE 
    document_type_code = 'TEST_DAPPER'";


        private const string InsertSql = @"INSERT INTO document_type (
    document_type_code,
    document_type_name 
) VALUES (
    ?DocumentTypeCode, 
    ?DocumentTypeName
)";


        private const string UpdateSql = @"UPDATE 
    document_type
SET
    document_type_name = ?DocumentTypeName
WHERE
    document_type_code = ?DocumentTypeCode";



        private const string DeleteSql = @"DELETE FROM document_type 
WHERE document_type_code = ?DocumentTypeCode";




        public static void DoTestSelect()
        {
            Console.WriteLine("Test Select MySql Data...");

            using (MySqlConnection conn = new MySqlConnection(Config.A0005_EF_Mysql_V6_ConnString))
            {
                conn.Open();


                Console.WriteLine("--- select all ---");
                var testDatas = conn.Query<DocumentType>(SelectAllSql);
                foreach (var test in testDatas)
                {
                    Console.WriteLine(test);
                }


                Console.WriteLine("--- select one ---");
                var testData = conn.QueryFirstOrDefault<DocumentType>(SelectOneSql);
                Console.WriteLine(testData);

            }
        }



        public static void DoTestInsert()
        {
            Console.WriteLine("Test Insert MySql Data...");

            using (MySqlConnection conn = new MySqlConnection(Config.A0005_EF_Mysql_V6_ConnString))
            {
                conn.Open();


                conn.Execute(InsertSql,
                    new DocumentType()
                    {
                        DocumentTypeCode = "TEST_DAPPER",
                        DocumentTypeName = "测试新增！"
                    });
            }
        }


        public static void DoTestUpdate()
        {
            Console.WriteLine("Test Update MySql Data...");

            using (MySqlConnection conn = new MySqlConnection(Config.A0005_EF_Mysql_V6_ConnString))
            {
                conn.Open();


                conn.Execute(UpdateSql,
                    new DocumentType()
                    {
                        DocumentTypeCode = "TEST_DAPPER",
                        DocumentTypeName = "测试更新！"
                    });
            }
        }


        public static void DoTestDelete()
        {
            Console.WriteLine("Test Delete MySql Data...");


            using (MySqlConnection conn = new MySqlConnection(Config.A0005_EF_Mysql_V6_ConnString))
            {
                conn.Open();


                conn.Execute(DeleteSql,
                    new
                    {
                        DocumentTypeCode = "TEST_DAPPER",
                    });
            }

        }


    }
}

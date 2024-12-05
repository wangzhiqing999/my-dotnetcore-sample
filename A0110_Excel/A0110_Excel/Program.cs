using System;
using System.Data;
using System.Linq;


namespace A0110_Excel
{
    class Program
    {
        static void Main(string[] args)
        {
            TestNpoiWriteExcel();


            TestExcelReader();



            Console.ReadKey();
        }







        private static void TestNpoiWriteExcel()
        {
            NpoiWriteExcel tester = new NpoiWriteExcel();
            tester.CreateExcel();


            DataSet ds = tester.ReadExcel();

            foreach (DataTable dt in ds.Tables)
            {
                Console.WriteLine("Sheet名：{0}", dt.TableName);

                foreach (DataColumn col in dt.Columns)
                {
                    Console.Write(col.ColumnName);
                    Console.Write("\t");
                }
                Console.WriteLine();


                foreach (DataRow row in dt.Rows)
                {
                    foreach (DataColumn col in dt.Columns)
                    {
                        Console.Write(row[col.ColumnName]);
                        Console.Write("\t");
                    }
                    Console.WriteLine();
                }
            }
        }







        private static void TestExcelReader()
        {

            Console.WriteLine("------------------------------");

            string testFilename = "test_Npoi.xls";

            ExcelReader tester = new ExcelReader();

            var sheetNames = tester.GetSheetNames(testFilename);
            Console.WriteLine("Sheet名：{0}", string.Join(",", sheetNames));

            Console.WriteLine("------------------------------");


            var sheetInfoList = tester.GetSheetInfo(testFilename);
            foreach (var item in sheetInfoList)
            {
                Console.WriteLine(item);
            }


            Console.WriteLine("------------------------------");

            var columns = tester.GetSheetDataColumns(testFilename, sheetNames[0]);
            Console.WriteLine("如果首行是标题：列名：{0}", string.Join(",", columns));
            
            Console.WriteLine("数据：");
            foreach(var item in tester.GetSheetData(testFilename, sheetNames[0]))
            {
                foreach(var key in item.Keys)
                {
                    Console.Write($"{key}={item[key]};");
                }
                Console.WriteLine();
            }


            Console.WriteLine("------------------------------");
            columns = tester.GetSheetDataColumns(testFilename, sheetNames[0], 0, false);
            Console.WriteLine("如果首行不是标题：列名：{0}", string.Join(",", columns));

            Console.WriteLine("数据：");
            foreach (var item in tester.GetSheetData(testFilename, sheetNames[0], 0, false))
            {
                foreach (var key in item.Keys)
                {
                    Console.Write($"{key}={item[key]};");
                }
                Console.WriteLine();
            }


        }



    }
}

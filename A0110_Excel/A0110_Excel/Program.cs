using System;
using System.Data;


namespace A0110_Excel
{
    class Program
    {
        static void Main(string[] args)
        {
            TestNpoiWriteExcel();



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



    }
}

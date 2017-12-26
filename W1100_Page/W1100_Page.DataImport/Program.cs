using System;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Collections.Generic;


using W1100_Page.Model;
using W1100_Page.DataAccess;

namespace W1100_Page.DataImport
{
    class Program
    {
        static void Main(string[] args)
        {

            ImportData(@"Data\20171218.xml");
            ImportData(@"Data\20171219.xml");
            ImportData(@"Data\20171220.xml");
            ImportData(@"Data\20171221.xml");
            ImportData(@"Data\20171222.xml");


            Console.WriteLine("Finish!");
            Console.ReadKey();
        }


        static void ImportData(string fileName)
        {
            if (!File.Exists(fileName))
            {
                Console.WriteLine("File Not Found!");
                return;
            }

            XmlSerializer xs = new XmlSerializer(typeof(List<FinanceData>));
            using (StreamReader sr = new StreamReader(fileName))
            {
                List<FinanceData> resultList = xs.Deserialize(sr) as List<FinanceData>;
                
                if(resultList.Count > 0)
                {
                    using(TestContext context = new TestContext())
                    {
                        foreach(var data in resultList)
                        {
                            context.FinanceDatas.Add(data);
                        }

                        context.SaveChanges();
                    }
                }
            }

            Console.WriteLine("{0} import finish!", fileName);
        }
    }
}

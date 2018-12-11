using System;
using System.Linq;


using A0007_EF_QueryFilter.DataAccess;
using A0007_EF_QueryFilter.Model;



namespace A0007_EF_QueryFilter
{
    class Program
    {
        static void Main(string[] args)
        {
            TestNewData();
            TestQueryData();


            Console.WriteLine("---------- Press Entey ----------");
            Console.ReadLine();


            TestLogicRemove();
            TestQueryData();


            Console.WriteLine("---------- Press Entey ----------");
            Console.ReadLine();


            TestRemove();
            TestQueryData();

            Console.WriteLine("Finish!");

            Console.ReadLine();
        }


        /// <summary>
        /// 测试新增数据.
        /// </summary>
        static void TestNewData()
        {
            Console.WriteLine("### Test New Data Start .");

            using(TestContext context = new TestContext())
            {
                TestData testData = new TestData()
                {
                    Name = "张三",
                    Phone = "13000000001",
                    Address = "上海市南京西路1168号",
                };
                context.TestDatas.Add(testData);



                DocumentType documentType1 = new DocumentType()
                {
                    DocumentTypeCode = "TEST1",
                    DocumentTypeName = "测试分类1",
                    IsActive = true,
                    CreateUser = "张三",
                    LastUpdateUser = "张三",
                    CreateTime = DateTime.Now,
                    LastUpdateTime = DateTime.Now
                };                
                DocumentType documentType2 = new DocumentType()
                {
                    DocumentTypeCode = "TEST2",
                    DocumentTypeName = "测试分类2",
                    IsActive = true,
                    CreateUser = "张三",
                    LastUpdateUser = "张三",
                    CreateTime = DateTime.Now,
                    LastUpdateTime = DateTime.Now
                };
                context.DocumentTypes.Add(documentType1);
                context.DocumentTypes.Add(documentType2);


                Document doc1 = new Document()
                {
                    DocumentTypeCode = "TEST1",
                    DocumentTitle = "测试文章1-标题",
                    DocumentContent = "测试文章1-内容",                    
                    IsActive = true,
                    CreateUser = "张三",
                    LastUpdateUser = "张三",
                    CreateTime = DateTime.Now,
                    LastUpdateTime = DateTime.Now
                };                
                Document doc2 = new Document()
                {
                    DocumentTypeCode = "TEST1",
                    DocumentTitle = "测试文章2-标题",
                    DocumentContent = "测试文章2-内容",
                    IsActive = true,
                    CreateUser = "张三",
                    LastUpdateUser = "张三",
                    CreateTime = DateTime.Now,
                    LastUpdateTime = DateTime.Now
                };
                Document doc3 = new Document()
                {
                    DocumentTypeCode = "TEST2",
                    DocumentTitle = "测试文章3-标题",
                    DocumentContent = "测试文章3-内容",
                    IsActive = true,
                    CreateUser = "张三",
                    LastUpdateUser = "张三",
                    CreateTime = DateTime.Now,
                    LastUpdateTime = DateTime.Now
                };
                
                context.Documents.Add(doc1);
                context.Documents.Add(doc2);
                context.Documents.Add(doc3);

                context.SaveChanges();
            }

            Console.WriteLine("### Test New Data Finish .");
        }


        /// <summary>
        /// 测试查询数据.
        /// </summary>
        static void TestQueryData()
        {
            Console.WriteLine("### Test Query Data Start .");

            using (TestContext context = new TestContext())
            {
                
                var testDataQuery =
                    from data in context.TestDatas
                    select data;
                foreach(var item in testDataQuery)
                {
                    Console.WriteLine(item);
                }
                Console.WriteLine("--------------------");

                var documentTypeQuery =
                    from data in context.DocumentTypes
                    select data;
                foreach (var item in documentTypeQuery)
                {
                    Console.WriteLine(item);
                }
                Console.WriteLine("--------------------");

                var documentQuery =
                    from data in context.Documents
                    select data;
                foreach (var item in documentQuery)
                {
                    Console.WriteLine(item);

                    if(item.DocumentType != null)
                    {
                        Console.WriteLine("+++ DocumentType is Not Null.");
                    }

                }
                Console.WriteLine("--------------------");

            }

            Console.WriteLine("### Test Query Data Finish .");
        }


        /// <summary>
        /// 测试逻辑删除.
        /// </summary>
        static void TestLogicRemove()
        {
            Console.WriteLine("### Test Logic Remove Data Start .");

            using (TestContext context = new TestContext())
            {
                DocumentType documentType2 = context.DocumentTypes.Find("TEST2");
                if(documentType2 != null)
                {
                    documentType2.IsActive = false;
                }


                Document doc2 = context.Documents.FirstOrDefault(p => p.DocumentTitle == "测试文章2-标题");
                if(doc2 != null)
                {
                    doc2.IsActive = false;
                }

                context.SaveChanges();
            }

            Console.WriteLine("### Test Logic Remove Data Finish .");
        }


        /// <summary>
        /// 测试物理删除.
        /// </summary>
        static void TestRemove()
        {
            Console.WriteLine("### Test Remove Data Start .");

            using (TestContext context = new TestContext())
            {
                var testDataList = context.TestDatas.ToList();
                foreach(var item in testDataList)
                {
                    context.TestDatas.Remove(item);
                }
                context.SaveChanges();

                var docList = context.Documents.ToList();
                foreach (var item in docList)
                {
                    context.Documents.Remove(item);
                }
                context.SaveChanges();

                var docTypeList = context.DocumentTypes.ToList();
                foreach (var item in docTypeList)
                {
                    context.DocumentTypes.Remove(item);
                }
                context.SaveChanges();
            }

            Console.WriteLine("### Test Remove Data Finish .");
        }
    }
}

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Text;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace W2001_Mvc_V8.Controllers
{
    public class TestDownloadController : Controller
    {



        private IWebHostEnvironment _WebHostingEnvironment;

        public TestDownloadController(IWebHostEnvironment hostingEnvironment)
        {
            this._WebHostingEnvironment = hostingEnvironment;
        }




        public IActionResult Index()
        {
            return View();
        }



        /// <summary>
        /// 服务器不产生 物理的文件。
        /// 数据是以一个 byte数组的方式， 向客户端返回。
        /// </summary>
        /// <returns></returns>
        public FileResult CsvFileInBytes()
        {
            string csvString = "A,B,C,D,E,F";
            return File(
                fileContents:Encoding.ASCII.GetBytes(csvString),
                contentType: "text/csv",
                fileDownloadName: "test.csv");
        }


        /// <summary>
        /// 服务器不产生 物理的文件。
        /// 数据是以一个 数据流的方式， 向客户端返回。
        /// </summary>
        /// <returns></returns>
        public FileResult CsvFileInStream()
        {
            string csvString = "A,B,C,D,E,F,1,2,3,4,5,6";

            MemoryStream memoryStream = new MemoryStream(Encoding.ASCII.GetBytes(csvString));

            return File(
                fileStream: memoryStream,
                contentType: "text/csv",
                fileDownloadName: "test1.csv");
        }


        /// <summary>
        /// 服务器产生一个临时文件，返回给客户端.
        /// </summary>
        /// <returns></returns>
        public FileResult CsvFileInOutput()
        {
            string csvString = "A,B,C,D,E,F,1,2,3,4,5,6,a,b,c,d,e,f";


            string wwwPath = this._WebHostingEnvironment.WebRootPath;

            // 存储文件的物理路径.
            string outputFilePath = $"{wwwPath}/Output";
            string outputFileName = $"{wwwPath}/Output/test2.csv";

            // 目录不存在，则创建.
            if (!System.IO.Directory.Exists(outputFilePath))
            {
                System.IO.Directory.CreateDirectory(outputFilePath);
            }

            System.IO.File.WriteAllText(outputFileName, csvString);


            return File(
                virtualPath: "/Output/test2.csv",
                contentType: "text/csv",
                fileDownloadName: "test2.csv");
        }





    }
}

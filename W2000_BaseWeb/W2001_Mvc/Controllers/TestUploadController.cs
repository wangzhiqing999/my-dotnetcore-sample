using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using W2001_Mvc.Models;


namespace W2001_Mvc.Controllers
{

    /// <summary>
    /// 测试上传.
    /// </summary>
    public class TestUploadController : Controller
    {


        private IWebHostEnvironment _WebHostingEnvironment;


        public TestUploadController(IWebHostEnvironment hostingEnvironment)
        {
            this._WebHostingEnvironment = hostingEnvironment;
        }



        public IActionResult Index()
        {
            return View();
        }



        #region 上传单个文件.

        /// <summary>
        /// 上传单个文件.
        /// </summary>
        /// <returns></returns>
        public IActionResult UploadOneFile()
        {
            return View();
        }


        /// <summary>
        /// ajax 上传单个文件.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult DoUploadOneFile()
        {
            var formData = Request.Form;

            if (formData.Files.Count == 0)
            {
                return Json(CommonServiceResult.CreateFailResult("FILE_NOT_FOUND", "文件未上传！"));
            }

            var formFile = Request.Form.Files[0];

            string wwwPath = this._WebHostingEnvironment.WebRootPath;

            // 存储文件的物理路径.
            string saveFilePath = $"{wwwPath}/Upload";
            string saveFileName = $"{wwwPath}/Upload/{formFile.FileName}";

            // 存储.

            // 目录不存在，则创建.
            if(!System.IO.Directory.Exists(saveFilePath))
            {
                System.IO.Directory.CreateDirectory(saveFilePath);
            }

            // 文件已存在，则删除.
            if(System.IO.File.Exists(saveFileName))
            {
                System.IO.File.Delete(saveFileName);
            }

            using (var stream = System.IO.File.Create(saveFileName))
            {
                formFile.CopyToAsync(stream);
            }

            return Json(CommonServiceResult.DefaultSuccessResult);
        }


        #endregion




        #region 上传两个文件.

        /// <summary>
        /// 上传两个文件.
        /// </summary>
        /// <returns></returns>
        public IActionResult UploadTwoFile()
        {
            return View();
        }


        /// <summary>
        /// ajax 上传两个文件.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult DoUploadTwoFile()
        {
            var formData = Request.Form;


            if (formData.Files.Count != 2)
            {
                return Json(CommonServiceResult.CreateFailResult("FILE_IS_LOST", "文件不足！"));
            }


            string wwwPath = this._WebHostingEnvironment.WebRootPath;


            foreach(var formFile in Request.Form.Files)
            {
                // 存储文件的物理路径.
                string saveFilePath = $"{wwwPath}/Upload/{formFile.Name}";
                // 目录不存在，则创建.
                if (!System.IO.Directory.Exists(saveFilePath))
                {
                    System.IO.Directory.CreateDirectory(saveFilePath);
                }

                string saveFileName = $"{wwwPath}/Upload/{formFile.Name}/{formFile.FileName}";

                // 文件已存在，则删除.
                if (System.IO.File.Exists(saveFileName))
                {
                    System.IO.File.Delete(saveFileName);
                }
                using (var stream = System.IO.File.Create(saveFileName))
                {
                    formFile.CopyToAsync(stream);
                }
            }

            return Json(CommonServiceResult.DefaultSuccessResult);
        }


        #endregion





        #region 混合上传


        public IActionResult UploaFileAndText()
        {
            return View();
        }


        [HttpPost]
        public JsonResult DoUploaFileAndText()
        {
            var formData = Request.Form;

            if (formData.Files.Count == 0)
            {
                return Json(CommonServiceResult.CreateFailResult("FILE_NOT_FOUND", "文件未上传！"));
            }

            string name = formData["name"];
            string mobile = formData["mobile"];

            var formFile = Request.Form.Files[0];

            string wwwPath = this._WebHostingEnvironment.WebRootPath;

            // 存储文件的物理路径.
            string saveFilePath = $"{wwwPath}/Upload";
            string saveFileName = $"{wwwPath}/Upload/{formFile.FileName}";

            // 存储.

            // 目录不存在，则创建.
            if (!System.IO.Directory.Exists(saveFilePath))
            {
                System.IO.Directory.CreateDirectory(saveFilePath);
            }

            // 文件已存在，则删除.
            if (System.IO.File.Exists(saveFileName))
            {
                System.IO.File.Delete(saveFileName);
            }

            using (var stream = System.IO.File.Create(saveFileName))
            {
                formFile.CopyToAsync(stream);
            }


            var resultData = new
            {
                name,
                mobile,
                photo = $"/Upload/{formFile.FileName}"
            };

            return Json(CommonServiceResult.CreateDefaultSuccessResult(resultData));

        }

        #endregion



    }
}

using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using W2002_Web_V8.Models;


namespace W2002_Web_V8.Pages
{


    public class UploadOneFileModel : PageModel
    {

        private IWebHostEnvironment _WebHostingEnvironment;


        public UploadOneFileModel(IWebHostEnvironment hostingEnvironment)
        {
            this._WebHostingEnvironment = hostingEnvironment;
        }


        public void OnGet()
        {
        }



        /// <summary>
        /// ajax 上传单个文件.
        /// </summary>
        /// <returns></returns>
        public async Task<JsonResult> OnPostAsync()
        {
            var formData = Request.Form;

            if (formData.Files.Count == 0)
            {
                JsonResult jsonResult = new JsonResult(CommonServiceResult.CreateFailResult("FILE_NOT_FOUND", "文件未上传！"));
                return jsonResult;
            }


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
                await formFile.CopyToAsync(stream);
            }

            return new JsonResult(CommonServiceResult.DefaultSuccessResult);
        }
    }


  
}

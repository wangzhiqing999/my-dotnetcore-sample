using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

using W1000_ABP_HelloWorld.SystemConfig;
using W1000_ABP_HelloWorld.SystemConfig.Dtos;
using W1000_ABP_HelloWorld.Web.Models.SystemConfig;

namespace W1000_ABP_HelloWorld.Web.Controllers
{

    // 直接创建 空白控制器， 生成代码会报错.
    // 只好简单创建一个类， 然后手动继承 W1000_ABP_HelloWorldControllerBase
    public class SystemConfigTypeController : W1000_ABP_HelloWorldControllerBase
    {

        /// <summary>
        /// 系统配置服务.
        /// </summary>
        private readonly ISystemConfigService _systemConfigService;



        public SystemConfigTypeController(ISystemConfigService systemConfigService)
        {
            _systemConfigService = systemConfigService;
        }



        public async Task<ActionResult> Index()
        {
            var output = await _systemConfigService.GetAllSystemConfigType();

            var model = new SystemConfigTypeIndexViewModel(output.Items);

            return View(model);
        }

    }
}

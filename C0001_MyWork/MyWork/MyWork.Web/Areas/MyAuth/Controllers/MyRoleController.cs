using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authorization;

using MyFramework.ServiceModel;

using MyWork.Web.Filters;

using MyAuthentication.Model;
using MyAuthentication.Service;
using MyAuthentication.ServiceModel;


namespace MyWork.Web.Areas.MyAuth.Controllers
{

    /// <summary>
    /// 角色服务.
    /// </summary>
    [EnableCors("AllowCors")]
    [Produces("application/json")]
    [Area("MyAuth")]
    [Authorize]
    public class MyRoleController : Controller
    {
        /// <summary>
        /// 角色服务.
        /// </summary>
        private IRoleService _RoleService;


        /// <summary>
        /// 角色-功能模块关系服务.
        /// </summary>
        private IRoleModuleService _RoleModuleService;


        /// <summary>
        /// 构造函数.
        /// </summary>
        /// <param name="roleService"></param>
        /// <param name="roleModuleService"></param>
        public MyRoleController(IRoleService roleService, IRoleModuleService roleModuleService)
        {
            this._RoleService = roleService;
            this._RoleModuleService = roleModuleService;
        }



        /// <summary>
        /// 查询角色列表
        /// </summary>
        /// <param name="systemCode">系统代码</param>
        /// <param name="pageNo">第几页</param>
        /// <param name="pageSize">每页几行</param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/MyAuth/MyRole")]
        public CommonQueryResult<MyRole> Query(string systemCode, int pageNo = 1, int pageSize = 10)
        {
            var result = this._RoleService.Query(systemCode, pageNo, pageSize);
            return result;
        }



        /// <summary>
        /// 获取角色.
        /// </summary>
        /// <param name="id">角色代码</param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/MyAuth/MyRole/Get/{id}")]
        public CommonServiceResult Get(string id)
        {
            var result = this._RoleService.GetRole(id);
            return result;
        }


        /// <summary>
        /// 新增角色.
        /// </summary>
        /// <param name="data">角色数据</param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/MyAuth/MyRole/Create")]
        [WithCreater]
        public CommonServiceResult Create([FromBody]MyRole data)
        {
            var result = this._RoleService.NewRole(data);
            return result;
        }



        /// <summary>
        /// 更新角色.
        /// </summary>
        /// <param name="data">角色数据</param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/MyAuth/MyRole/Update")]
        [WithLastUpdater]
        public CommonServiceResult Update([FromBody]MyRole data)
        {
            var result = this._RoleService.UpdateRole(data);
            return result;
        }



        /// <summary>
        /// 删除角色.
        /// </summary>
        /// <param name="data">角色代码</param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/MyAuth/MyRole/Delete")]
        public CommonServiceResult Delete([FromBody]RemoveRequest data)
        {
            string id = Convert.ToString(data.id);
            var result = this._RoleService.RemoveRole(id);
            return result;
        }







        /// <summary>
        /// 获取角色可访问的模块..
        /// </summary>
        /// <param name="id">角色代码</param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/MyAuth/MyRole/GetManagerAbleModule/{id}")]
        public List<ManagerAbleModule> GetManagerAbleModule(string id)
        {
            var result = this._RoleModuleService.GetManagerAbleModuleByRoleCode(id);
            return result;
        }


        /// <summary>
        /// 更新角色可访问的模块.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/MyAuth/MyRole/UpdateManagerAbleModule/{id}")]
        public CommonServiceResult UpdateManagerAbleModule(string id, [FromBody]List<ManagerAbleModule> data)
        {            
            var result = this._RoleModuleService.UpdateManagerAbleModule(id, data);
            return result;
        }


    }
}
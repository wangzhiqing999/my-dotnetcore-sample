using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;

using MyFramework.ServiceModel;

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
    public class MyRoleController : Controller
    {
        /// <summary>
        /// 角色服务.
        /// </summary>
        private IRoleService _RoleService;


        /// <summary>
        /// 构造函数.
        /// </summary>
        /// <param name="roleService"></param>
        public MyRoleController(IRoleService roleService)
        {
            this._RoleService = roleService;
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



    }
}
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

using MyAuthentication.Model;
using MyAuthentication.DataAccess;
using MyAuthentication.Service;
using MyFramework.ServiceModel;

namespace MyAuthentication.ServiceImpl
{

    /// <summary>
    /// 角色-功能模块关系服务.
    /// </summary>
    public class DefaultRoleModuleServiceImpl : IRoleModuleService
    {

        List<MyModule> IRoleModuleService.GetModuleByRoleCode(string roleCode)
        {
            using (MyAuthenticationContext context = new MyAuthenticationContext())
            {
                var query =
                    from data in context.MyRoleModules
                    where
                        data.RoleCode == roleCode
                    select data.Module;

                List<MyModule> resultList = query.ToList();
                return resultList;
            }
        }


        CommonServiceResult IRoleModuleService.UpdateRolesModule(string roleCode, List<string> moduleCodeList)
        {
            try
            {
                using (MyAuthenticationContext context = new MyAuthenticationContext())
                {
                    var query =
                        from data in context.MyRoleModules
                        where
                            data.RoleCode == roleCode
                        select data;

                    List<MyRoleModule> dbList = query.ToList();

                    // 待删除列表.
                    List<MyRoleModule> removeList = new List<MyRoleModule>();
                    // 待添加列表.
                    List<MyRoleModule> addList = new List<MyRoleModule>();


                    foreach(string moduleCode in moduleCodeList)
                    {
                        if(!dbList.Exists(p=>p.ModuleCode == moduleCode)) {
                            // 本次需要更新的模块， 数据库中没有. 需要做新增的操作.
                            MyRoleModule newData = new MyRoleModule();
                            newData.ModuleCode = moduleCode;
                            newData.RoleCode = roleCode;
                            addList.Add(newData);
                        } 
                    }
                    foreach(var dbItem in dbList)
                    {
                        if(!moduleCodeList.Contains(dbItem.ModuleCode))
                        {
                            // 数据库中的模块， 本次需要更新的列表中，不存在，需要做 删除的操作.
                            removeList.Add(dbItem);
                        }
                    }

                    // 开始处理.
                    // 先删除.
                    foreach(var removeData in removeList)
                    {
                        context.MyRoleModules.Remove(removeData);
                    }
                    // 后追加.
                    foreach (var addData in addList)
                    {
                        context.MyRoleModules.Add(addData);
                    }

                    // 物理保存.
                    context.SaveChanges();
                }

                return CommonServiceResult.DefaultSuccessResult;
            }
            catch (Exception ex)
            {
                return new CommonServiceResult(ex);
            }
        }
    }
}

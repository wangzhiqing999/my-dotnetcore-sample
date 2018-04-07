using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;

using MyAuthentication.Model;
using MyAuthentication.DataAccess;
using MyAuthentication.Service;
using MyFramework.ServiceModel;
using MyAuthentication.ServiceModel;

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



        List<ManagerAbleModule> IRoleModuleService.GetManagerAbleModuleByRoleCode(string roleCode)
        {
            // 结果列表.
            List<ManagerAbleModule> resultList = new List<ManagerAbleModule>();

            using (MyAuthenticationContext context = new MyAuthenticationContext())
            {
                // 查询角色.
                MyRole role = context.MyRoles.Find(roleCode);
                if(role == null)
                {
                    // 角色不存在，返回空白列表.
                    return resultList;
                }

                // 查询角色所在系统下的所有模块.
                var allDataQuery =
                    from data in context.MyModules.Include("Actions")
                    where
                        data.SystemCode == role.SystemCode
                    orderby
                        data.ModuleTypeCode, data.ModuleCode                    
                    select
                        data;
                List<MyModule> allModuleList = allDataQuery.ToList();

                // 查询角色所分配的模块.
                var moduleQuery =
                    from data in context.MyRoleModules
                    where
                        data.RoleCode == roleCode
                    select data.ModuleCode;
                List<string> moduleCodeList = moduleQuery.ToList();

                // 查询角色所分配的动作.
                var actionQuery =
                    from data in context.MyRoleActions
                    where
                        data.RoleCode == roleCode
                    select data.ActionCode;
                List<string> actionCodeList = actionQuery.ToList();

                // 遍历全部.
                foreach(MyModule module in allModuleList)
                {
                    ManagerAbleModule item = new ManagerAbleModule(module);
                    // 模块是否可访问.
                    if(moduleCodeList.Contains(item.ModuleCode))
                    {
                        item.AccessAble = true;
                        // 模块可访问的情况下， 查询动作.
                        foreach(var action in item.Actions)
                        {
                            action.AccessAble = actionCodeList.Contains(action.ActionCode);
                        }
                    }
                    // 加入结果列表.
                    resultList.Add(item);
                }
            }

            return resultList;

        }




        CommonServiceResult IRoleModuleService.UpdateManagerAbleModule(string roleCode, List<ManagerAbleModule> dataList)
        {

            try
            {
                using (MyAuthenticationContext context = new MyAuthenticationContext())
                {
                    // 查询角色.
                    MyRole role = context.MyRoles.Find(roleCode);
                    if (role == null)
                    {
                        // 角色不存在，返回.
                        return AuthenticationServiceResult.RoleCodeNotFoundResult;
                    }

                    // 先删除当前角色的所有  模块/动作 关联.
                    var removeRoleModuleList = context.MyRoleModules.Where(p => p.RoleCode == roleCode).ToList();
                    var removeRoleAcrionList = context.MyRoleActions.Where(p => p.RoleCode == roleCode).ToList();
                    context.MyRoleActions.RemoveRange(removeRoleAcrionList);
                    context.MyRoleModules.RemoveRange(removeRoleModuleList);
                    context.SaveChanges();


                    // 再添加新的 角色的 模块/动作 关联.
                    foreach (var module in dataList)
                    {
                        if(module.AccessAble)
                        {
                            // 模块可访问.
                            MyRoleModule roleModule = new MyRoleModule()
                            {
                                ModuleCode = module.ModuleCode,
                                RoleCode = roleCode,
                            };
                            context.MyRoleModules.Add(roleModule);

                            foreach(var action in module.Actions)
                            {
                                if(action.AccessAble)
                                {
                                    MyRoleAction roleAction = new MyRoleAction()
                                    {
                                        ActionCode = action.ActionCode,
                                        RoleCode = roleCode,
                                    };
                                    context.MyRoleActions.Add(roleAction);
                                }
                            }
                        }
                    }
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

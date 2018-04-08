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
    public class DefaultUserSystemServiceImpl : IUserSystemService
    {


        public List<ManagerAbleSystem> GetManagerAbleSystemByUserID(long userID)
        {
            // 结果列表.
            List<ManagerAbleSystem> resultList = new List<ManagerAbleSystem>();

            using (MyAuthenticationContext context = new MyAuthenticationContext())
            {
                // 查询用户.
                MyUser user = context.MyUsers.Find(userID);
                if (user == null)
                {
                    // 用户不存在，返回空白列表.
                    return resultList;
                }


                // 查询所有的系统， 与系统下的角色.
                var allDataQuery =
                    from data in context.MySystems.Include("Roles")
                    orderby
                        data.SystemCode
                    select
                        data;
                List<MySystem> allSystemList = allDataQuery.ToList();


                // 查询用户所能访问的系统
                var systemQuery =
                    from data in context.MySystemUsers
                    where
                        data.UserID == userID
                    select data.SystemCode;
                List<string> systemCodeList = systemQuery.ToList();

                // 查询用户所分配的角色.
                var roleQuery =
                    from data in context.MyUserRoles
                    where
                        data.UserID == userID
                    select data.RoleCode;
                List<string> roleCodeList = roleQuery.ToList();


                // 遍历全部.
                foreach (MySystem system in allSystemList)
                {
                    ManagerAbleSystem item = new ManagerAbleSystem(system);

                    // 用户是否可访问当前系统.
                    if (systemCodeList.Contains(item.SystemCode))
                    {
                        item.AccessAble = true;
                        // 模块可访问的情况下， 查询分配的角色.
                        foreach (var role in item.Roles)
                        {
                            role.AccessAble = roleCodeList.Contains(role.RoleCode);
                        }
                    }
                    // 加入结果列表.
                    resultList.Add(item);
                }

            }

            return resultList;
        }




        public CommonServiceResult UpdateManagerAbleSystem(long userID, List<ManagerAbleSystem> dataList)
        {
            try
            {
                using (MyAuthenticationContext context = new MyAuthenticationContext())
                {
                    // 查询用户.
                    MyUser user = context.MyUsers.Find(userID);
                    if (user == null)
                    {
                        // 用户不存在，返回
                        return AuthenticationServiceResult.UserIDNotFoundResult;
                    }


                    // 先删除当前用户的所有  系统/角色 关联.
                    var removeSystemUserList = context.MySystemUsers.Where(p => p.UserID == userID).ToList();
                    var removeUserRoleList = context.MyUserRoles.Where(p => p.UserID == userID).ToList();
                    context.MySystemUsers.RemoveRange(removeSystemUserList);
                    context.MyUserRoles.RemoveRange(removeUserRoleList);
                    context.SaveChanges();


                    // 再添加新的 用户的 系统/角色 关联.
                    foreach (var system in dataList)
                    {
                        if(system.AccessAble)
                        {
                            // 用户允许访问该系统.
                            MySystemUser systemUser = new MySystemUser()
                            {
                                SystemCode = system.SystemCode,
                                UserID = userID
                            };
                            context.MySystemUsers.Add(systemUser);

                            foreach(var role in system.Roles)
                            {
                                if(role.AccessAble)
                                {
                                    // 用户被分配了角色.
                                    MyUserRole userRole = new MyUserRole()
                                    {
                                        RoleCode = role.RoleCode,
                                        UserID = userID
                                    };
                                    context.MyUserRoles.Add(userRole);
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

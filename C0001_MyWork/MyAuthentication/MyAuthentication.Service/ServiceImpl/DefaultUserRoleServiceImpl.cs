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
    public class DefaultUserRoleServiceImpl : IUserRoleService
    {
        List<MyRole> IUserRoleService.GetRoleByUserID(long userID)
        {
            using (MyAuthenticationContext context = new MyAuthenticationContext())
            {
                var query =
                    from data in context.MyUserRoles
                    where
                        data.UserID == userID
                    select data.Role;

                List<MyRole> resultList = query.ToList();
                return resultList;
            }
        }

        CommonServiceResult IUserRoleService.UpdateUsersRole(long userID, List<string> roleCodeList)
        {
            try
            {
                using (MyAuthenticationContext context = new MyAuthenticationContext())
                {
                    var query =
                        from data in context.MyUserRoles
                        where
                            data.UserID == userID
                        select data;

                    List<MyUserRole> dbList = query.ToList();

                    // 待删除列表.
                    List<MyUserRole> removeList = new List<MyUserRole>();
                    // 待添加列表.
                    List<MyUserRole> addList = new List<MyUserRole>();


                    foreach (string roleCode in roleCodeList)
                    {
                        if (!dbList.Exists(p => p.RoleCode == roleCode))
                        {
                            // 本次需要更新的角色， 数据库中没有. 需要做新增的操作.
                            MyUserRole newData = new MyUserRole();
                            newData.UserID = userID;
                            newData.RoleCode = roleCode;
                            addList.Add(newData);
                        }
                    }
                    foreach (var dbItem in dbList)
                    {
                        if (!roleCodeList.Contains(dbItem.RoleCode))
                        {
                            // 数据库中的角色， 本次需要更新的列表中，不存在，需要做 删除的操作.
                            removeList.Add(dbItem);
                        }
                    }

                    // 开始处理.
                    // 先删除.
                    foreach (var removeData in removeList)
                    {
                        context.MyUserRoles.Remove(removeData);
                    }
                    // 后追加.
                    foreach (var addData in addList)
                    {
                        context.MyUserRoles.Add(addData);
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

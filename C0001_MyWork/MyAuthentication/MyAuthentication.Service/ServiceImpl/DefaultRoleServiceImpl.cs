using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

using MyFramework.ServiceImpl;
using MyFramework.ServiceModel;

using MyAuthentication.Model;
using MyAuthentication.DataAccess;
using MyAuthentication.Service;


using MyAuthentication.ServiceModel;


namespace MyAuthentication.ServiceImpl
{

    /// <summary>
    /// 角色服务实现.
    /// </summary>
    public class DefaultRoleServiceImpl : DefaultCommonService, IRoleService
    {

        List<MyRole> IRoleService.GetAllRoles()
        {
            using(MyAuthenticationContext context = new MyAuthenticationContext())
            {
                var query =
                    from data in context.MyRoles
                    select data;

                List<MyRole> resultList = query.ToList();
                return resultList;
            }
        }



        MyRole IRoleService.GetRole(string id)
        {
            using (MyAuthenticationContext context = new MyAuthenticationContext())
            {
                var query =
                    from data in context.MyRoles
                    where
                        data.RoleCode == id
                    select data;

                MyRole result = query.FirstOrDefault();
                return result;
            }
        }

        CommonServiceResult IRoleService.NewRole(MyRole role)
        {
            try
            {
                using (MyAuthenticationContext context = new MyAuthenticationContext())
                {
                    var query =
                        from data in context.MyRoles
                        where
                            data.RoleCode == role.RoleCode
                        select data;

                    if (query.Count() > 0)
                    {
                        // 角色代码已存在.
                        CommonServiceResult errResult = AuthenticationServiceResult.RoleCodeHadExistsResult;
                        return errResult;
                    }
                    context.MyRoles.Add(role);
                    context.SaveChanges();
                }
                return CommonServiceResult.DefaultSuccessResult;

            }
            catch (Exception ex)
            {
                return new CommonServiceResult(ex);
            }
        }


        CommonServiceResult IRoleService.RemoveRole(string roleCode)
        {
            try
            {
                using (MyAuthenticationContext context = new MyAuthenticationContext())
                {
                    MyRole dbData = context.MyRoles.Find(roleCode);
                    if (dbData == null)
                    {
                        // 角色代码不存在.
                        CommonServiceResult errResult = AuthenticationServiceResult.RoleCodeNotFoundResult;
                        return errResult;
                    }
                    context.MyRoles.Remove(dbData);
                    context.SaveChanges();
                }
                return CommonServiceResult.DefaultSuccessResult;

            }
            catch (Exception ex)
            {
                return new CommonServiceResult(ex);
            }
        }


        CommonServiceResult IRoleService.UpdateRole(MyRole role)
        {
            try
            {
                using (MyAuthenticationContext context = new MyAuthenticationContext())
                {
                    MyRole dbData = context.MyRoles.Find(role.RoleCode);
                    if (dbData == null)
                    {
                        // 角色代码不存在.
                        CommonServiceResult errResult = AuthenticationServiceResult.RoleCodeNotFoundResult;
                        return errResult;
                    }

                    base.UpdateProperty<MyRole>(role, dbData);

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

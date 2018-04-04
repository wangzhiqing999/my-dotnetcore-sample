using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

using MyFramework.ServiceImpl;
using MyFramework.ServiceModel;
using MyFramework.Util;

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

        CommonQueryResult<MyRole> IRoleService.Query(string systemCode, int pageNo, int pageSize)
        {
            using(MyAuthenticationContext context = new MyAuthenticationContext())
            {
                var query =
                    from data in context.MyRoles
                    select data;

                if(!String.IsNullOrEmpty(systemCode))
                {
                    query = query.Where(p => p.SystemCode == systemCode);
                }

                // 初始化翻页.
                PageInfo pgInfo = new PageInfo(
                    pageSize: pageSize,
                    pageNo: pageNo,
                    rowCount: query.Count());

                // 翻页.
                query = query.OrderByDescending(p => p.LastUpdateTime)
                    .Skip(pgInfo.SkipValue)
                    .Take(pgInfo.PageSize);

                List<MyRole> dataList = query.ToList();


                CommonQueryResult<MyRole> result = new CommonQueryResult<MyRole>()
                {
                    QueryPageInfo = pgInfo,
                    QueryResultData = dataList
                };

                return result;
            }
        }



        CommonServiceResult IRoleService.GetRole(string id)
        {
            using (MyAuthenticationContext context = new MyAuthenticationContext())
            {
                var query =
                    from data in context.MyRoles
                    where
                        data.RoleCode == id
                    select data;

                MyRole roleData = query.FirstOrDefault();

                if (roleData == null)
                {
                    // 数据不存在.
                    return CommonServiceResult.DataNotFoundResult;
                }


                CommonServiceResult result = CommonServiceResult.CreateDefaultSuccessResult(roleData);
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

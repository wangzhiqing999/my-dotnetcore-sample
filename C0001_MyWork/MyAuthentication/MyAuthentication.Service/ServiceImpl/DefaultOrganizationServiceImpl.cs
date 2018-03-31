using System;
using System.Collections.Generic;
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
    public class DefaultOrganizationServiceImpl : DefaultCommonService, IOrganizationService
    {

        MyOrganization IOrganizationService.GetOrganization(long id)
        {
            using (MyAuthenticationContext context = new MyAuthenticationContext())
            {
                MyOrganization result = context.MyOrganizations.Find(id);
                return result;
            }
        }



        CommonServiceResult IOrganizationService.NewOrganization(MyOrganization organization)
        {
            try
            {
                using (MyAuthenticationContext context = new MyAuthenticationContext())
                {
                    MyOrganization dbData = context.MyOrganizations.Find(organization.OrganizationID);
                    if (dbData != null)
                    {
                        // 组织ID已存在.
                        CommonServiceResult errResult = AuthenticationServiceResult.OrganizationIDHadExistsResult;
                        return errResult;
                    }


                    dbData = context.MyOrganizations.FirstOrDefault(p => p.LoginOrganizationCode == organization.LoginOrganizationCode);
                    if (dbData != null)
                    {
                        // 组织代码已存在.
                        CommonServiceResult errResult = AuthenticationServiceResult.OrganizationCodeHadExistsResult;
                        return errResult;
                    }


                    context.MyOrganizations.Add(organization);
                    context.SaveChanges();
                }

                return CommonServiceResult.DefaultSuccessResult;
            }
            catch (Exception ex)
            {
                return new CommonServiceResult(ex);
            }
        }



        CommonQueryResult<MyOrganization> IOrganizationService.Query(int pageNo, int pageSize)
        {
            using (MyAuthenticationContext context = new MyAuthenticationContext())
            {
                var query =
                    from data in context.MyOrganizations
                    select data;


                // 初始化翻页.
                PageInfo pgInfo = new PageInfo(
                    pageSize: pageSize,
                    pageNo: pageNo,
                    rowCount: query.Count());

                // 翻页.
                query = query.OrderByDescending(p => p.LastUpdateTime)
                    .Skip(pgInfo.SkipValue)
                    .Take(pgInfo.PageSize);

                List<MyOrganization> dataList = query.ToList();


                CommonQueryResult<MyOrganization> result = new CommonQueryResult<MyOrganization>()
                {
                    QueryPageInfo = pgInfo,
                    QueryResultData  = dataList
                };

                return result;
            }
        }


        CommonServiceResult IOrganizationService.RemoveOrganization(long id)
        {
            try
            {
                using (MyAuthenticationContext context = new MyAuthenticationContext())
                {
                    MyOrganization dbData = context.MyOrganizations.Find(id);
                    if (dbData == null)
                    {
                        // 组织ID不存在.
                        CommonServiceResult errResult = AuthenticationServiceResult.OrganizationIDNotFoundResult;
                        return errResult;
                    }

                    var query =
                        from data in context.MyUsers
                        where
                            data.OrganizationID == id
                        select
                            data;
                    if(query.Count() > 0)
                    {
                        // 组织下存在有用户数据
                        CommonServiceResult errResult = AuthenticationServiceResult.OrganizationExistsSubUserResult;
                        return errResult;
                    }

                    context.MyOrganizations.Remove(dbData);
                    context.SaveChanges();
                }

                return CommonServiceResult.DefaultSuccessResult;
            }
            catch (Exception ex)
            {
                return new CommonServiceResult(ex);
            }
        }




        CommonServiceResult IOrganizationService.UpdateOrganization(MyOrganization organization)
        {
            try
            {
                using (MyAuthenticationContext context = new MyAuthenticationContext())
                {
                    MyOrganization dbData = context.MyOrganizations.Find(organization.OrganizationID);
                    if (dbData == null)
                    {
                        // 组织ID不存在.
                        CommonServiceResult errResult = AuthenticationServiceResult.OrganizationIDNotFoundResult;
                        return errResult;
                    }


                    if(organization.LoginOrganizationCode != dbData.LoginOrganizationCode)
                    {
                        // 不允许修改组织代码
                        CommonServiceResult errResult = AuthenticationServiceResult.OrganizationCodeModifyResult;
                        return errResult;
                    }


                    base.UpdateProperty<MyOrganization>(organization, dbData);
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

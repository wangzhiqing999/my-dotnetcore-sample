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
    public class DefaultRoleActionServiceImpl : IRoleActionService
    {

        private readonly MyAuthenticationContext context;


        public DefaultRoleActionServiceImpl(MyAuthenticationContext context)
        {
            this.context = context;
        }





        List<MyAction> IRoleActionService.GetActionByRoleCode(string roleCode)
        {
            //using (MyAuthenticationContext context = new MyAuthenticationContext())
            {
                var query =
                    from data in context.MyRoleActions
                    where
                        data.RoleCode == roleCode
                    select data.Action;

                List<MyAction> resultList = query.ToList();
                return resultList;
            }
        }

        CommonServiceResult IRoleActionService.UpdateRolesAction(string roleCode, List<string> actionCodeList)
        {
            try
            {
                //using (MyAuthenticationContext context = new MyAuthenticationContext())
                {
                    var query =
                        from data in context.MyRoleActions
                        where
                            data.RoleCode == roleCode
                        select data;

                    List<MyRoleAction> dbList = query.ToList();

                    // 待删除列表.
                    List<MyRoleAction> removeList = new List<MyRoleAction>();
                    // 待添加列表.
                    List<MyRoleAction> addList = new List<MyRoleAction>();


                    foreach (string actionCode in actionCodeList)
                    {
                        if (!dbList.Exists(p => p.ActionCode == actionCode))
                        {
                            // 本次需要更新的模块动作， 数据库中没有. 需要做新增的操作.
                            MyRoleAction newData = new MyRoleAction();
                            newData.ActionCode = actionCode;
                            newData.RoleCode = roleCode;
                            addList.Add(newData);
                        }
                    }
                    foreach (var dbItem in dbList)
                    {
                        if (!actionCodeList.Contains(dbItem.ActionCode))
                        {
                            // 数据库中的模块动作， 本次需要更新的列表中，不存在，需要做 删除的操作.
                            removeList.Add(dbItem);
                        }
                    }

                    // 开始处理.
                    // 先删除.
                    foreach (var removeData in removeList)
                    {
                        context.MyRoleActions.Remove(removeData);
                    }
                    // 后追加.
                    foreach (var addData in addList)
                    {
                        context.MyRoleActions.Add(addData);
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

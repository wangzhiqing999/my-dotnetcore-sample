﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

using MyAuthentication.Model;
using MyAuthentication.DataAccess;
using MyAuthentication.Service;


namespace MyAuthentication.ServiceImpl
{
    public class DefaultActionServiceImpl : IActionService
    {

        private readonly MyAuthenticationContext context;


        public DefaultActionServiceImpl(MyAuthenticationContext context)
        {
            this.context = context;
        }



        List<MyAction> IActionService.GetActionList(string moduleCode)
        {
            var query =
                from data in context.MyActions
                where
                    data.ModuleCode == moduleCode
                select data;

            List<MyAction> resultList = query.ToList();
            return resultList;            
        }
    }
}

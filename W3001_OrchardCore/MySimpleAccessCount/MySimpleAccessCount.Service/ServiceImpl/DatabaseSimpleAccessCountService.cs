using System;
using System.Collections.Generic;
using System.Text;

using MySimpleAccessCount.Model;
using MySimpleAccessCount.DataAccess;
using MySimpleAccessCount.Service;

namespace MySimpleAccessCount.ServiceImpl
{
    public class DatabaseSimpleAccessCountService : ISimpleAccessCountService
    {

        private readonly MySimpleAccessCountContext _Context;


        public DatabaseSimpleAccessCountService(MySimpleAccessCountContext context)
        {
            this._Context = context;
        }

        public long AccessCount(string pageCode)
        {
            PageAccessCount pageData = this._Context.PageAccessCounts.Find(pageCode);
            if(pageData == null)
            {
                return -1;
            }

            try
            {
                // 真实访问数量递增.
                pageData.RealAccessCount = pageData.RealAccessCount + 1;
                if (pageData.IsSaveDailyCount)
                {
                    // 如果需要存储每日明细的情况下.

                    PageDailyAccessCount dailyData = this._Context.PageDailyAccessCounts.Find(pageCode, DateTime.Today);
                    if(dailyData == null)
                    {
                        dailyData = new PageDailyAccessCount()
                        {
                            PageCode = pageCode,
                            AccessDate = DateTime.Today,
                            RealAccessCount = 1,
                        };
                        this._Context.PageDailyAccessCounts.Add(dailyData);
                    } 
                    else
                    {
                        dailyData.RealAccessCount = dailyData.RealAccessCount + 1;
                    }

                }
                this._Context.SaveChanges();
            } 
            catch(Exception)
            {
                // 忽略异常.
            }
            
            return pageData.DisplayAccessCount;
        }
    }
}

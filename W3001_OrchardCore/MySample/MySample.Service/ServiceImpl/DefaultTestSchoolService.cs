using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

using MySample.Model;
using MySample.DataAccess;

using MySample.Service;


namespace MySample.ServiceImpl
{
    /// <summary>
    /// 测试学校服务
    /// </summary>
    public class DefaultTestSchoolService : ITestSchoolService
    {

        private readonly MySampleContext _Context;


        public DefaultTestSchoolService(MySampleContext context)
        {
            this._Context = context;
        }



        public void CreateTestSchool(TestSchool data)
        {
            this._Context.TestSchools.Add(data);
            this._Context.SaveChanges();
        }

        public List<TestSchool> GetTestSchoolList()
        {
            List<TestSchool> resultList = this._Context.TestSchools.ToList();
            return resultList;
        }
    }
}

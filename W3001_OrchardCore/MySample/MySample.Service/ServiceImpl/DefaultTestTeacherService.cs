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
    /// 测试老师服务.
    /// </summary>
    public class DefaultTestTeacherService : ITestTeacherService
    {


        private readonly MySampleContext _Context;


        public DefaultTestTeacherService(MySampleContext context)
        {
            this._Context = context;
        }



        public void CreateTeacher(TestTeacher data)
        {
            this._Context.TestTeachers.Add(data);
            this._Context.SaveChanges();
        }

        public List<TestTeacher> GetTestTeacher()
        {
            var query =
                from data in this._Context.TestTeachers
                select
                    data;

            var resultList = query.ToList();
            return resultList;
        }

        public List<TestTeacher> GetTestTeacherInSchool(long schoolID)
        {
            var query =
                from data in this._Context.TestTeachers
                where
                    data.SchoolID == schoolID
                select
                    data;

            var resultList = query.ToList();
            return resultList;
        }
    }

}

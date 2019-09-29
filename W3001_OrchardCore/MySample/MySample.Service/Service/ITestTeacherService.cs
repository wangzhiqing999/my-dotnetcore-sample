using System;
using System.Collections.Generic;
using System.Text;

using MySample.Model;


namespace MySample.Service
{

    /// <summary>
    /// 测试老师服务
    /// </summary>
    public interface ITestTeacherService
    {

        /// <summary>
        /// 获取全部老师列表.
        /// </summary>
        /// <returns></returns>
        List<TestTeacher> GetTestTeacher();


        /// <summary>
        /// 获取学校下的老师列表.
        /// </summary>
        /// <param name="schoolID"></param>
        /// <returns></returns>
        List<TestTeacher> GetTestTeacherInSchool(long schoolID);


        /// <summary>
        /// 创建老师.
        /// </summary>
        /// <param name="data"></param>
        void CreateTeacher(TestTeacher data);


    }
}

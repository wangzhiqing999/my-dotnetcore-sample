using System;
using System.Collections.Generic;
using System.Text;

using MySample.Model;


namespace MySample.Service
{

    /// <summary>
    /// 测试学校服务.
    /// </summary>
    public interface ITestSchoolService
    {

        /// <summary>
        /// 获取学校列表.
        /// </summary>
        /// <returns></returns>
        List<TestSchool> GetTestSchoolList();


        /// <summary>
        /// 创建学校.
        /// </summary>
        /// <param name="data"></param>
        void CreateTestSchool(TestSchool data);


    }
}

using System;
using System.Collections.Generic;
using System.Text;

using Abp.Domain.Repositories;
using W1010_ABP_NetCode2.Tasks.Dtos;


namespace W1010_ABP_NetCode2.Tasks
{

    /// <summary>
    /// 测试用户类型服务.
    /// </summary>
    public class TestUserTypeAppService : W1010_ABP_NetCode2AppServiceBase, ITestUserTypeAppService
    {

        private readonly IRepository<Other, Int64> _otherRepository;


        private readonly IRepository<TestUserType, string> _testUserTypeRepository;



        public TestUserTypeAppService(
            IRepository<TestUserType, string> testUserTypeRepository, 
            IRepository<Other, Int64> otherRepository)
        {
            _testUserTypeRepository = testUserTypeRepository;
            _otherRepository = otherRepository;
        }




        /// <summary>
        /// 创建用户类型
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public void Create(CreateTestUserTypeInput input)
        {
            TestUserType userType1 = ObjectMapper.Map<TestUserType>(input);
            TestUserType userType2 = ObjectMapper.Map<TestUserType>(input);


            userType1.Id = "TEST";
            userType2.Id = "TEST";



            Other other = new Other()
            {
                Name = "test_transaction",                
            };
            _otherRepository.Insert(other);



            // 下面的操作， 将导致 插入2条主键相同的数据， 而导致异常.
            // 测试的目的， 是 调用本方法， 抛出异常后， 事务处理的原因。
            // 最终数据库， 存储 TestUserType 的表中，应该没有主键为 TEST 的数据.  存储 Other 的表中，没有 名称为 test_transaction 的数据。
            _testUserTypeRepository.Insert(userType1);
            _testUserTypeRepository.Insert(userType2);


        }


    }
}

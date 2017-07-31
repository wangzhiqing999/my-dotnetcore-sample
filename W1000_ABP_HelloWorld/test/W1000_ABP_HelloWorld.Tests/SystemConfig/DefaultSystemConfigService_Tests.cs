using Shouldly;
using Xunit;
using System.Linq;
using Abp.Runtime.Validation;


using W1000_ABP_HelloWorld.SystemConfig;
using W1000_ABP_HelloWorld.SystemConfig.Dtos;


namespace W1000_ABP_HelloWorld.Tests.SystemConfig
{
    public class DefaultSystemConfigService_Tests : W1000_ABP_HelloWorldTestBase
    {

        private readonly ISystemConfigService _systemConfigService;



        public DefaultSystemConfigService_Tests()
        {
            _systemConfigService = Resolve<ISystemConfigService>();
        }



        [Fact]
        public async System.Threading.Tasks.Task GetAllSystemConfigType_Test()
        {
            //Act
            var output = await _systemConfigService.GetAllSystemConfigType();

            //Assert
            output.Items.Count.ShouldBe(3);
        }




        [Fact]
        public async System.Threading.Tasks.Task GetSystemConfigType_Test()
        {
            // 查询参数.
            ConfigTypeCodeInput input1 = new ConfigTypeCodeInput() { Id = "NOT EXISTS" };
            // 执行查询.
            var output1 = await _systemConfigService.GetSystemConfigType(input1);
            // 不存在的代码， 结果应该为空.
            Assert.Null(output1);


            // 查询参数.
            ConfigTypeCodeInput input2 = new ConfigTypeCodeInput() { Id = "TEST_STRING" };
            // 执行查询.
            var output2 = await _systemConfigService.GetSystemConfigType(input2);
            // 存在的代码， 结果应该非空.
            Assert.NotNull(output2);
            // 名称检查.
            output2.ConfigTypeName.ShouldBe<string>("简单测试String");
        }




        [Fact]
        public async System.Threading.Tasks.Task GetSystemConfigPropertyByType_Test()
        {
            // 查询参数.
            ConfigTypeCodeInput input1 = new ConfigTypeCodeInput() { Id = "NOT EXISTS" };
            // 执行查询.
            var output1 = await _systemConfigService.GetSystemConfigPropertyByType(input1);
            // 不存在的代码， 结果非空.
            Assert.NotNull(output1);
            // 长度为0.
            output1.Items.Count.ShouldBe(0);



            // 查询参数.
            ConfigTypeCodeInput input2 = new ConfigTypeCodeInput() { Id = "TEST_STRING" };
            // 执行查询.
            var output2 = await _systemConfigService.GetSystemConfigPropertyByType(input2);
            // 存在的代码， 结果非空.
            Assert.NotNull(output2);
            // 长度为0.
            output2.Items.Count.ShouldBe(0);


            // 查询参数.
            ConfigTypeCodeInput input3 = new ConfigTypeCodeInput() { Id = "TEST_DICTIONARY_1" };
            // 执行查询.
            var output3 = await _systemConfigService.GetSystemConfigPropertyByType(input3);
            // 存在的代码， 结果非空.
            Assert.NotNull(output3);
            // 长度为4.
            output3.Items.Count.ShouldBe(4);

        }




        [Fact]
        public async System.Threading.Tasks.Task GetSystemConfigValueByType_Test()
        {
            // 查询参数.
            ConfigTypeCodeInput input1 = new ConfigTypeCodeInput() { Id = "NOT EXISTS" };
            // 执行查询.
            var output1 = await _systemConfigService.GetSystemConfigValueByType(input1);
            // 不存在的代码， 结果非空.
            Assert.NotNull(output1);
            // 长度为0.
            output1.Items.Count.ShouldBe(0);


            // 查询参数.
            ConfigTypeCodeInput input2 = new ConfigTypeCodeInput() { Id = "TEST_STRING" };
            // 执行查询.
            var output2 = await _systemConfigService.GetSystemConfigValueByType(input2);
            // 存在的代码， 结果非空.
            Assert.NotNull(output2);
            // 长度为3.
            output2.Items.Count.ShouldBe(3);

        }




        [Fact]
        public async System.Threading.Tasks.Task GetSystemConfigValue_Test()
        {
            GetSystemConfigValueInput input1 = new GetSystemConfigValueInput()
            {
                ConfigTypeCode = "TEST_STRING",
                ConfigCode ="NOT EXISTS!"
            };
            // 执行查询.
            var output1 = await _systemConfigService.GetSystemConfigValue(input1);
            // 不存在的代码， 结果为空.
            Assert.Null(output1);


            GetSystemConfigValueInput input2 = new GetSystemConfigValueInput()
            {
                ConfigTypeCode = "TEST_STRING",
                ConfigCode = "TEST_STRING_2"
            };
            // 执行查询.
            var output2 = await _systemConfigService.GetSystemConfigValue(input2);
            // 存在的代码， 结果非空.
            Assert.NotNull(output2);

            output2.ConfigName.ShouldBe<string>("测试字符串信息2");
            output2.ConfigValue.ShouldBe<string>("Test String 2.");

        }



    }
}

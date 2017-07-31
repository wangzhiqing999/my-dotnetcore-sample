using W1000_ABP_HelloWorld.EntityFrameworkCore;


using W1000_ABP_HelloWorld.SystemConfig;


namespace W1000_ABP_HelloWorld.Tests.TestDatas
{
    public class TestDataBuilder
    {
        private readonly W1000_ABP_HelloWorldDbContext _context;

        public TestDataBuilder(W1000_ABP_HelloWorldDbContext context)
        {
            _context = context;
        }



        /// <summary>
        /// ¥¥Ω®≤‚ ‘ ˝æ›.
        /// </summary>
        public void Build()
        {

            var configType1 = new SystemConfigType()
            {
                Id = "TEST_STRING",
                ConfigTypeName = "ºÚµ•≤‚ ‘String",
                ConfigClassName = "System.String"
            };
            var configType2 = new SystemConfigType()
            {
                Id = "TEST_INT32",
                ConfigTypeName = "ºÚµ•≤‚ ‘Int32",
                ConfigClassName = "System.Int32"
            };
            var configType3 = new SystemConfigType()
            {
                Id = "TEST_DICTIONARY_1",
                ConfigTypeName = "ºÚµ•≤‚ ‘Dictionary",
                ConfigClassName = "System.Collections.Generic.Dictionary`2[System.String,System.Object]"
            };


            _context.SystemConfigTypes.Add(configType1);
            _context.SystemConfigTypes.Add(configType2);
            _context.SystemConfigTypes.Add(configType3);
            _context.SaveChanges();



            var configProp1 = new SystemConfigProperty()
            {
                ConfigTypeCode = "TEST_DICTIONARY_1",
                PropertyName = "Name",
                PropertyDataType = "System.String",
                PropertyDesc = "–’√˚",
                DisplayOrder = 1
            };
            var configProp2 = new SystemConfigProperty()
            {
                ConfigTypeCode = "TEST_DICTIONARY_1",
                PropertyName = "Age",
                PropertyDataType = "System.Int32",
                PropertyDesc = "ƒÍ¡‰",
                DisplayOrder = 2
            };
            var configProp3 = new SystemConfigProperty()
            {
                ConfigTypeCode = "TEST_DICTIONARY_1",
                PropertyName = "Address",
                PropertyDataType = "System.String",
                PropertyDesc = "µÿ÷∑",
                DisplayOrder = 3
            };
            var configProp4 = new SystemConfigProperty()
            {
                ConfigTypeCode = "TEST_DICTIONARY_1",
                PropertyName = "IsEmployee",
                PropertyDataType = "System.Boolean",
                PropertyDesc = " «∑ÒπÕ‘±",
                DisplayOrder = 4
            };

            _context.SystemConfigPropertys.Add(configProp1);
            _context.SystemConfigPropertys.Add(configProp2);
            _context.SystemConfigPropertys.Add(configProp3);
            _context.SystemConfigPropertys.Add(configProp4);
            _context.SaveChanges();




            var configValue1 = new SystemConfigValue()
            {
                ConfigTypeCode = "TEST_STRING",
                ConfigCode = "TEST_STRING_1",
                ConfigName = "≤‚ ‘◊÷∑˚¥Æ–≈œ¢1",
                ConfigValue = "Test String 1.",
            };
            var configValue2 = new SystemConfigValue()
            {
                ConfigTypeCode = "TEST_STRING",
                ConfigCode = "TEST_STRING_2",
                ConfigName = "≤‚ ‘◊÷∑˚¥Æ–≈œ¢2",
                ConfigValue = "Test String 2.",
            };
            var configValue3 = new SystemConfigValue()
            {
                ConfigTypeCode = "TEST_STRING",
                ConfigCode = "TEST_STRING_3",
                ConfigName = "≤‚ ‘◊÷∑˚¥Æ–≈œ¢3",
                ConfigValue = "Test String 3.",
            };

            _context.SystemConfigValues.Add(configValue1);
            _context.SystemConfigValues.Add(configValue2);
            _context.SystemConfigValues.Add(configValue3);
            _context.SaveChanges();

        }
    }
}
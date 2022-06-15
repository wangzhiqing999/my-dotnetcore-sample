
using B0100_FluentValidation.Model;
using B0100_FluentValidation.Validator;

namespace B0100_FluentValidation
{
    internal class Program
    {
        static void Main(string[] args)
        {

            ProcNull();

            ProcEmpty();

            ProcLength();

            Console.WriteLine("##### Finish #####");
            Console.ReadLine();
        }






        static void ProcNull()
        {
            Console.WriteLine("----- Null 检查处理 -----");

            UserInformation userInformation = new UserInformation();

            userInformation.UserName = null;
            userInformation.Sex = null;
            userInformation.CardNo = null;
            userInformation.Age = 18;
            userInformation.Email = null;

            UserInformationValidator validationRules = new UserInformationValidator();
            var result = validationRules.Validate(userInformation);
            if (!result.IsValid)
            {
                Console.WriteLine(string.Join(Environment.NewLine, result.Errors.Select(x => x.ErrorMessage).ToArray()));
            }
        }


        static void ProcEmpty()
        {
            Console.WriteLine("----- Empty 检查处理 -----");

            UserInformation userInformation = new UserInformation();

            userInformation.UserName = string.Empty;
            userInformation.Sex = string.Empty;
            userInformation.CardNo = string.Empty;
            userInformation.Age = 18;
            userInformation.Email = string.Empty;

            UserInformationValidator validationRules = new UserInformationValidator();
            var result = validationRules.Validate(userInformation);
            if (!result.IsValid)
            {
                Console.WriteLine(string.Join(Environment.NewLine, result.Errors.Select(x => x.ErrorMessage).ToArray()));
            }
        }

        static void ProcLength()
        {
            Console.WriteLine("----- Length 检查处理 -----");

            UserInformation userInformation = new UserInformation();

            userInformation.UserName = "丹妮莉丝 坦格利安，旧瓦雷利亚的后裔，安达尔人先民的女王，维斯特洛的统治者暨全境守护者，大草原多斯拉克人卡丽熙，不焚者，弥林的女王，镣拷打破者，龙之母，阿斯塔波的解放者，罗伊拿人和先民的女王，龙石岛公主。";
            userInformation.Sex = "女";
            userInformation.CardNo = "12345678901234567890";
            userInformation.Age = 24;
            userInformation.Email = "test@test.com";

            UserInformationValidator validationRules = new UserInformationValidator();
            var result = validationRules.Validate(userInformation);
            if (!result.IsValid)
            {
                Console.WriteLine(string.Join(Environment.NewLine, result.Errors.Select(x => x.ErrorMessage).ToArray()));
            }
        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FluentValidation;

using B0100_FluentValidation.Model;


namespace B0100_FluentValidation.Validator
{
    public class UserInformationValidator : AbstractValidator<UserInformation>
    {

        public UserInformationValidator()
        {



            // 使用 NotNull 验证程序
            // 确保指定的属性不是 null。
            RuleFor(o => o.UserName)
                .NotNull().WithMessage("姓名不能为空")
                .NotEmpty().WithMessage("姓名必须要填写");

            // MaxLength 最大长度验证程序
            // 确保特定字符串属性的长度不超过指定的值。
            RuleFor(o => o.UserName)
                .MaximumLength(32).WithMessage("姓名长度太长了！");





            // 使用 Must 验证程序
            // 将指定属性的值传递到一个委托中, 可以对该值执行自定义验证逻辑
            RuleFor(o => o.Sex).Must(o => o == "男" || o == "女").WithMessage("性别输入错误");






            // 使用 NotNull 验证程序
            // 确保指定的属性不是 null。
            RuleFor(o => o.CardNo)
                .NotNull().WithMessage("证件号码不能为空");

            // 使用 Length 长度验证程序
            // 确保特定字符串属性的长度位于指定范围内。但是, 它不能确保字符串属性是否为 null。
            RuleFor(o => o.CardNo)
                .Length(18, 18).WithMessage("证件号码长度不正确");






            RuleFor(o => o.Age).ExclusiveBetween(0, 200).WithMessage("年龄输入错误");




            // Email 电子邮件验证程序
            // 确保指定属性的值是有效的电子邮件地址格式。
            RuleFor(o => o.Email).EmailAddress().WithMessage("邮箱输入错误");


        }


    }

}

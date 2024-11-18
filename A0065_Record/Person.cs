using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A0065_Record
{

    /// <summary>
    /// 使用标准的 clsss 的写法.
    /// </summary>
    internal class OldPerson
    {
        public OldPerson(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public string FirstName { set; get; }
        public string LastName { set; get; }

        public override string ToString()
        {
            return $"OldPerson FirstName = {FirstName}, LastName = {LastName}";
        }


        public void Print()
        {
            Console.WriteLine("#####");
            Console.WriteLine($"{FirstName} {LastName}");
            Console.WriteLine("#####");
        }

    }



    /// <summary>
    /// 使用 record 的写法.
    /// <br/>
    /// 自动生成一些对我们有用的成员函数.
    /// 构造函数：根据定义的属性自动生成构造函数。
    /// 属性：自动生成只读属性。
    /// Equals 和 GetHashCode 方法：基于属性值的相等性比较。
    /// ToString 方法：提供友好的字符串表示,对于调试输出特别友好。
    /// </summary>
    /// <param name="FirstName"></param>
    /// <param name="LastName"></param>
    internal record NewPerson
    (
        string FirstName, 
        string LastName
    );





    /// <summary>
    /// 填充既有类
    /// <br/>
    /// 对于已有的类，我们可以使用 record class 来填充。
    /// </summary>
    internal record class OldPersonPlus
    {
        public string FirstName { set; get; }
        public string LastName { set; get; }

        public void Print()
        {
            Console.WriteLine("#####");
            Console.WriteLine($"{FirstName} {LastName}");
            Console.WriteLine("#####");
        }
    }


}

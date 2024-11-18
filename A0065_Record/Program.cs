namespace A0065_Record
{
    internal class Program
    {

        static void Main(string[] args)
        {

            OldPerson oldPerson1 = new OldPerson("Hello", "World!");
            Console.WriteLine($"oldPerson1 = {oldPerson1}");

            NewPerson newPerson1 = new NewPerson("Hello", "World!");
            Console.WriteLine($"newPerson1 = {newPerson1}");


            OldPerson oldPerson2 = new OldPerson("Hello", "World!");
            Console.WriteLine($"oldPerson2 = {oldPerson2}");

            NewPerson newPerson2 = new NewPerson("Hello", "World!");
            Console.WriteLine($"newPerson2 = {newPerson2}");



            // Record 的特性.
            // 我们很多时候有这种需求就是比较一个类的所有属性来判断逻辑.
            // 如果使用 record 的话 我们只需要==或者Equals就能判断,
            Console.WriteLine("----- == -----");
            Console.WriteLine($"oldPerson1 == oldPerson2: {oldPerson1 == oldPerson2}");
            Console.WriteLine($"newPerson1 == newPerson2: {newPerson1 == newPerson2}");




            // 非破坏性复制值.
            Console.WriteLine("----- with -----");
            NewPerson newPerson3 = newPerson2 with { FirstName = "New" };
            Console.WriteLine($"newPerson2 = {newPerson2}");
            Console.WriteLine($"newPerson3 = {newPerson3}");


            // 下面的写法，将编译错误。
            //var newPerson4 = newPerson2 with { };
            //newPerson4.FirstName = "NewNew";





            // 填充既有类.
            Console.WriteLine("----- record class -----");
            // record class 不会根据定义的属性自动生成构造函数， 只能使用类的写法.
            OldPersonPlus oldPersonPlus = new OldPersonPlus { FirstName="Hello", LastName="World"};

            // 测试 record class 生成 ToString 方法
            Console.WriteLine($"oldPersonPlus = {oldPersonPlus}");


            // 测试 record class 生成 Equals 方法
            var oldPersonPlus2 = oldPersonPlus with {};
            Console.WriteLine($"oldPersonPlus2 = {oldPersonPlus2}");
            Console.WriteLine($"oldPersonPlus == oldPersonPlus2: {oldPersonPlus == oldPersonPlus2}");

            // 测试 record class 非破坏性复制值.
            var oldPersonPlus3 = oldPersonPlus with { FirstName = "New" };
            Console.WriteLine($"oldPersonPlus3 = {oldPersonPlus3}");


            var oldPersonPlus4 = oldPersonPlus with { };
            oldPersonPlus4.FirstName = "NewNew";
            Console.WriteLine($"oldPersonPlus4 = {oldPersonPlus4}");

            oldPersonPlus4.Print();

            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
    }




}

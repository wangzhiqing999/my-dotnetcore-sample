using System;
using DynamicExpresso;

namespace B0120_DynamicExpresso
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Test1();
            Test2();
            Test3();
            Test4();
            Test5();
            Test6();

            Console.WriteLine("-----");
            Console.ReadLine();
        }

        /// <summary>
        /// 简单表达式.
        /// </summary>
        private static void Test1()
        {
            string express = "10 + 2 * 3";

            var interpreter = new Interpreter();
            var result = interpreter.Eval(express);

            Console.WriteLine($"{express} = {result}");
            Console.WriteLine();
        }

        /// <summary>
        /// 使用变量
        /// </summary>
        private static void Test2()
        {
            string express = "x + y";
            int x = 10;
            int y = 5;

            var interpreter = new Interpreter();

            interpreter.SetVariable("x", x);
            interpreter.SetVariable("y", y);

            var result = interpreter.Eval(express);

            Console.WriteLine($"x = {x}");
            Console.WriteLine($"y = {y}");

            Console.WriteLine($"{express} = {result}");
            Console.WriteLine();
        }

        /// <summary>
        /// 自定义函数
        /// </summary>
        private static void Test3()
        {
            string express = "Add(10, 20)";

            var interpreter = new Interpreter();
            interpreter.SetFunction("Add", (Func<int, int, int>)((a, b) => a + b));

            var result = interpreter.Eval(express);

            Console.WriteLine($"{express} = {result}");
            Console.WriteLine();
        }

        /// <summary>
        /// 用于逻辑判断.
        /// </summary>
        private static void Test4()
        {
            string rule = "age > 18 && hasLicense";

            int age = 20;
            bool hasLicense = true;

            var interpreter = new Interpreter();

            interpreter.SetVariable("age", age);
            interpreter.SetVariable("hasLicense", hasLicense);

            var result = interpreter.Eval(rule);

            Console.WriteLine($"age = {age}");
            Console.WriteLine($"hasLicense = {hasLicense}");

            Console.WriteLine($"{rule} = {result}");
            Console.WriteLine();
        }

        /// <summary>
        /// 动态脚本执行.
        /// </summary>
        private static void Test5()
        {
            var script = "Print(\"Hello, \" + name)";

            var interpreter = new Interpreter();

            interpreter.SetVariable("name", "World");
            interpreter.SetFunction("Print", (Action<string>)(message => Console.WriteLine(message)));
            interpreter.Eval(script); // Output: Hello, World

            Console.WriteLine();
        }






        class User
        {
            public int Id { get; set; }
            public string? Name { get; set; }
            public int Age { get; set; }
        }

        /// <summary>
        /// 生成 Lambda 表达式.
        /// </summary>
        private static void Test6()
        {

            // Create some example user data  
            var users = new List<User>
            {
                new User { Id = 1, Name = "Alice", Age = 30 },
                new User { Id = 2, Name = "Bob", Age = 25 },
                new User { Id = 3, Name = "Charlie", Age = 35 },
                new User { Id = 4, Name = "David", Age = 28 }
            };

            // Convert user data to IQueryable  
            var queryableUsers = users.AsQueryable();

            // Define the filtering condition entered by the user  
            string userInputCondition = "user.Age > 28";

            // Use Dynamic Expresso to parse and compile the user-entered condition  
            var interpreter = new Interpreter();
            var lambda = interpreter.ParseAsExpression<Func<User, bool>>(userInputCondition, "user");

            // Filter user data using the parsed expression  
            var filteredUsers = queryableUsers.Where(lambda).ToList();

            // Output filtered user data  
            foreach (var user in filteredUsers)
            {
                Console.WriteLine($"Id: {user.Id}, Name: {user.Name}, Age: {user.Age}");
            }
        }



    }
}
using Flee.PublicTypes;
using Flee.CalcEngine.PublicTypes;

namespace B0110_Flee
{
    internal class Program
    {
        static void Main(string[] args)
        {

            SimpleExpression();

            VarExpression();

            CombinExpression();

            CustomFunctionsExpression();

            ParamsFunctionsExpression();


            BooleanExpression();


            Console.WriteLine("----- Finish -----");
            Console.ReadLine();
        }








        /// <summary>
        /// 简单的使用.
        /// 这种简单表达式内容都已经确定，全是可以计算的，没有变量。
        /// </summary>
        static void SimpleExpression()
        {
            Console.WriteLine("##### 简单使用 #####");

            // 表达式.
            string formulaString = "2 * (3 + 5) - 3.14";

            // 表达式上下文.
            ExpressionContext context = new ExpressionContext();
            // 允许表达式使用 System.Math 的所有静态公共方法.
            context.Imports.AddType(typeof(Math));


            IDynamicExpression myDynamic = context.CompileDynamic(formulaString);
            var result = myDynamic.Evaluate();

            Console.WriteLine($"{formulaString} = {result}");
            Console.WriteLine();

        }



        /// <summary>
        /// 带变量的使用.
        /// 也就是 表达式里面， 存在有 变量。
        /// 计算的时候， 需要额外传递 变量的值。
        /// </summary>
        static void VarExpression()
        {
            Console.WriteLine("##### 带变量使用 #####");

            // 表达式.
            string formulaString = "a * (b + 5) - sqrt(c)";

            // 表达式上下文.
            ExpressionContext context = new ExpressionContext();
            // 允许表达式使用 System.Math 的所有静态公共方法.
            context.Imports.AddType(typeof(Math));

            // 设置具体变量的数值.
            context.Variables["a"] = 2;
            context.Variables["b"] = 3;
            context.Variables["c"] = 4;


            IDynamicExpression myDynamic = context.CompileDynamic(formulaString);
            var result = myDynamic.Evaluate();


            foreach(var p in context.Variables)
            {
                Console.WriteLine($"{p.Key} = {p.Value}");
            }
            Console.WriteLine($"{formulaString} = {result}");
            Console.WriteLine();
        }





        /// <summary>
        /// 表达式组合使用.
        /// 简单来说， 就是 
        /// 表达式1 = ...
        /// 表达式2 = ...
        /// 结果 = 表达式1的结果 与 表达式2的结果做计算.
        /// 
        /// 应用场景：这种情况一般用于有多个表达式的场景，就类似于Excel中多个单元格中的表达式一样，
        /// 每个单元格中都有表达式，然后有一个单元格就会引用其他单元格中的表达式计算出来的值，最后得出结果。
        /// </summary>
        static void CombinExpression()
        {
            Console.WriteLine("##### 表达式组合使用 #####");


            // 计算引擎.
            CalculationEngine engine = new CalculationEngine();


            // 表达式上下文.
            ExpressionContext context = new ExpressionContext();
            // 允许表达式使用 System.Math 的所有静态公共方法.
            context.Imports.AddType(typeof(Math));


            // 表达式1.
            string formulaString1 = "a * (b + 5) - sqrt(c)";

            // 表达式2.
            string formulaString2 = "max(b, c) + d";



            // 设置具体变量的数值.
            context.Variables["a"] = 2;
            context.Variables["b"] = 3;
            context.Variables["c"] = 4;
            context.Variables["d"] = 5;




            // 将表达式1 ， 重新起个名字，加入到引擎中，这里为 X。
            engine.Add("X", formulaString1, context);

            // 将表达式2 ， 重新起个名字，加入到引擎中，这里为 Y。
            engine.Add("Y", formulaString2, context);



            // 组合引擎的表达式. 这里相当于 Z = X + Y.
            engine.Add("Z", "X + Y", context);

            // 计算结果.
            var result = engine.GetResult("Z");



            Console.WriteLine($"{engine.GetExpression("Z").ToString()} = {result}");
            Console.WriteLine();


        }







        /// <summary>
        /// 带 自定义函数 的使用.
        /// 也就是 表达式里面， 存在有 额外编写的 自定义函数。
        /// 
        /// </summary>
        static void CustomFunctionsExpression()
        {
            Console.WriteLine("##### 调用自定义函数 #####");

            // 表达式.
            string formulaString = "Add(a, b) + Mul(c, d)";

            // 表达式上下文.
            ExpressionContext context = new ExpressionContext();
            // 允许表达式使用 System.Math 的所有静态公共方法.
            context.Imports.AddType(typeof(Math));


            // ### 允许表达式使用自定义的静态公共方法.
            context.Imports.AddType(typeof(CustomFunctions));



            // 设置具体变量的数值.
            context.Variables["a"] = 2;
            context.Variables["b"] = 3;
            context.Variables["c"] = 4;
            context.Variables["d"] = 5;


            IDynamicExpression myDynamic = context.CompileDynamic(formulaString);
            var result = myDynamic.Evaluate();


            foreach (var p in context.Variables)
            {
                Console.WriteLine($"{p.Key} = {p.Value}");
            }
            Console.WriteLine($"{formulaString} = {result}");
            Console.WriteLine();
        }




        /// <summary>
        /// 带 可变参数的自定义函数 的使用.
        /// 也就是 表达式里面， 存在有 额外编写的 自定义函数。
        /// 这个自定义的函数， 参数的数量，还是可变的.
        /// </summary>
        static void ParamsFunctionsExpression()
        {
            Console.WriteLine("##### 调用自定义函数. 参数可变 #####");

            // 表达式.
            string formulaString = "Sum(a, b, c, d)";

            // 表达式上下文.
            ExpressionContext context = new ExpressionContext();
            // 允许表达式使用 System.Math 的所有静态公共方法.
            context.Imports.AddType(typeof(Math));


            // ### 允许表达式使用自定义的静态公共方法.
            context.Imports.AddType(typeof(CustomFunctions));



            // 设置具体变量的数值.
            context.Variables["a"] = 2;
            context.Variables["b"] = 3;
            context.Variables["c"] = 4;
            context.Variables["d"] = 5;


            IDynamicExpression myDynamic = context.CompileDynamic(formulaString);
            var result = myDynamic.Evaluate();


            foreach (var p in context.Variables)
            {
                Console.WriteLine($"{p.Key} = {p.Value}");
            }
            Console.WriteLine($"{formulaString} = {result}");
            Console.WriteLine();
        }






        /// <summary>
        /// 返回值是 布尔类型的.
        /// 
        /// 应用场景：
        /// 这种情况一般用于一些自动判定规则的需求，
        /// 比如有一些报表的结果在不同的业务类型时会有一个值的范围，
        /// 如果超出范围就代表不合规，靠人为判断的话可能就没那么高效；
        /// 类似这种对比数据的情况，如果只是简单的比对还容易实现，如果是复杂一点的关系比对，这种方式就相对灵活了。
        /// </summary>
        static void BooleanExpression()
        {

            Console.WriteLine("##### 返回值为布尔 #####");

            // 表达式.
            // 注意： 不等于要使用 <>.  写 != 会发生异常.
            string formulaString = "(a = 1 OR b >2) AND  c <> 3";

            // 表达式上下文.
            ExpressionContext context = new ExpressionContext();
            // 允许表达式使用 System.Math 的所有静态公共方法.
            context.Imports.AddType(typeof(Math));


            // 设置具体变量的数值.
            context.Variables["a"] = 2;
            context.Variables["b"] = 3;
            context.Variables["c"] = 4;


            IGenericExpression<bool> myDynamic = context.CompileGeneric<bool>(formulaString);


            var result = myDynamic.Evaluate();


            foreach (var p in context.Variables)
            {
                Console.WriteLine($"{p.Key} = {p.Value}");
            }
            Console.WriteLine($"{formulaString} = {result}");
            Console.WriteLine();
        }




    }
}
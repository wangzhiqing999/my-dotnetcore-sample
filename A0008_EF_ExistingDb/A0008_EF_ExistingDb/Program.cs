using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

using A0008_EF_ExistingDb.DataAccess;
using A0008_EF_ExistingDb.Model;

namespace A0008_EF_ExistingDb
{
    class Program
    {
        static void Main(string[] args)
        {

            using(MyCustomActionContext context = new MyCustomActionContext())
            {
                var query =
                    from data in context.McaServiceModule.Include("McaCustomAction")
                    select data;


                foreach(var module in query)
                {
                    Console.WriteLine( $"{module.ModuleCode} : {module.ModuleName}" );
                    foreach(var action in module.McaCustomAction)
                    {
                        Console.WriteLine($"   {action.Id} : {action.EnterTime}");
                    }
                }

            }



            Console.WriteLine("Finish");
            Console.ReadLine();
        }
    }
}

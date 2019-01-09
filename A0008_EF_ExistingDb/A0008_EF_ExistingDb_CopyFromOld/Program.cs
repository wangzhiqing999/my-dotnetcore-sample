using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

using A0008_EF_ExistingDb_CopyFromOld.DataAccess;
using A0008_EF_ExistingDb_CopyFromOld.Model;


namespace A0008_EF_ExistingDb_CopyFromOld
{
    class Program
    {
        static void Main(string[] args)
        {
            using (MyCustomActionContext context = new MyCustomActionContext())
            {
                var query =
                    from data in context.ServiceModules.Include("CustomActionList")
                    select data;


                foreach (var module in query)
                {
                    Console.WriteLine($"{module.ModuleCode} : {module.ModuleName}");
                    foreach (var action in module.CustomActionList)
                    {
                        Console.WriteLine($"   {action.ID} : {action.EnterTime}");
                    }
                }

            }



            Console.WriteLine("Finish");
            Console.ReadLine();
        }
    }
}

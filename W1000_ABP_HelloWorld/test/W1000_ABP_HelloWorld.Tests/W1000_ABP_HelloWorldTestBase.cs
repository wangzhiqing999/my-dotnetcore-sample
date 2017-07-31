using System;
using System.Threading.Tasks;
using Abp.TestBase;
using W1000_ABP_HelloWorld.EntityFrameworkCore;
using W1000_ABP_HelloWorld.Tests.TestDatas;

namespace W1000_ABP_HelloWorld.Tests
{
    public class W1000_ABP_HelloWorldTestBase : AbpIntegratedTestBase<W1000_ABP_HelloWorldTestModule>
    {
        public W1000_ABP_HelloWorldTestBase()
        {
            UsingDbContext(context => new TestDataBuilder(context).Build());
        }

        protected virtual void UsingDbContext(Action<W1000_ABP_HelloWorldDbContext> action)
        {
            using (var context = LocalIocManager.Resolve<W1000_ABP_HelloWorldDbContext>())
            {
                action(context);
                context.SaveChanges();
            }
        }

        protected virtual T UsingDbContext<T>(Func<W1000_ABP_HelloWorldDbContext, T> func)
        {
            T result;

            using (var context = LocalIocManager.Resolve<W1000_ABP_HelloWorldDbContext>())
            {
                result = func(context);
                context.SaveChanges();
            }

            return result;
        }

        protected virtual async Task UsingDbContextAsync(Func<W1000_ABP_HelloWorldDbContext, Task> action)
        {
            using (var context = LocalIocManager.Resolve<W1000_ABP_HelloWorldDbContext>())
            {
                await action(context);
                await context.SaveChangesAsync(true);
            }
        }

        protected virtual async Task<T> UsingDbContextAsync<T>(Func<W1000_ABP_HelloWorldDbContext, Task<T>> func)
        {
            T result;

            using (var context = LocalIocManager.Resolve<W1000_ABP_HelloWorldDbContext>())
            {
                result = await func(context);
                context.SaveChanges();
            }

            return result;
        }
    }
}

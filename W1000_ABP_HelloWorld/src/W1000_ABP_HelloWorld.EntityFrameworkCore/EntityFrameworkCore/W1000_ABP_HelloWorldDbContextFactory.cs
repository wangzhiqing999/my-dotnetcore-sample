using W1000_ABP_HelloWorld.Configuration;
using W1000_ABP_HelloWorld.Web;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;

namespace W1000_ABP_HelloWorld.EntityFrameworkCore
{
    /* This class is needed to run EF Core PMC commands. Not used anywhere else */
    public class W1000_ABP_HelloWorldDbContextFactory : IDbContextFactory<W1000_ABP_HelloWorldDbContext>
    {
        public W1000_ABP_HelloWorldDbContext Create(DbContextFactoryOptions options)
        {
            var builder = new DbContextOptionsBuilder<W1000_ABP_HelloWorldDbContext>();
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());

            DbContextOptionsConfigurer.Configure(
                builder, 
                configuration.GetConnectionString(W1000_ABP_HelloWorldConsts.ConnectionStringName)
                );

            return new W1000_ABP_HelloWorldDbContext(builder.Options);
        }
    }
}
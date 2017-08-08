using W1001_ABP_With_Zero.Configuration;
using W1001_ABP_With_Zero.Web;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;

namespace W1001_ABP_With_Zero.EntityFrameworkCore
{
    /* This class is needed to run "dotnet ef ..." commands from command line on development. Not used anywhere else */
    public class W1001_ABP_With_ZeroDbContextFactory : IDbContextFactory<W1001_ABP_With_ZeroDbContext>
    {
        public W1001_ABP_With_ZeroDbContext Create(DbContextFactoryOptions options)
        {
            var builder = new DbContextOptionsBuilder<W1001_ABP_With_ZeroDbContext>();
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());

            W1001_ABP_With_ZeroDbContextConfigurer.Configure(builder, configuration.GetConnectionString(W1001_ABP_With_ZeroConsts.ConnectionStringName));
            
            return new W1001_ABP_With_ZeroDbContext(builder.Options);
        }
    }
}
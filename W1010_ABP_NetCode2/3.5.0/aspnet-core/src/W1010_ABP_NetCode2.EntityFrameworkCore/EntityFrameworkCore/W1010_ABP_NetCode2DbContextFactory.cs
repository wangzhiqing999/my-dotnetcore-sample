using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using W1010_ABP_NetCode2.Configuration;
using W1010_ABP_NetCode2.Web;

namespace W1010_ABP_NetCode2.EntityFrameworkCore
{
    /* This class is needed to run "dotnet ef ..." commands from command line on development. Not used anywhere else */
    public class W1010_ABP_NetCode2DbContextFactory : IDesignTimeDbContextFactory<W1010_ABP_NetCode2DbContext>
    {
        public W1010_ABP_NetCode2DbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<W1010_ABP_NetCode2DbContext>();
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());

            W1010_ABP_NetCode2DbContextConfigurer.Configure(builder, configuration.GetConnectionString(W1010_ABP_NetCode2Consts.ConnectionStringName));

            return new W1010_ABP_NetCode2DbContext(builder.Options);
        }
    }
}

using Microsoft.EntityFrameworkCore;

namespace W1000_ABP_HelloWorld.EntityFrameworkCore
{
    public static class DbContextOptionsConfigurer
    {
        public static void Configure(
            DbContextOptionsBuilder<W1000_ABP_HelloWorldDbContext> dbContextOptions, 
            string connectionString
            )
        {
            /* This is the single point to configure DbContextOptions for W1000_ABP_HelloWorldDbContext */
            dbContextOptions.UseSqlServer(connectionString);
        }
    }
}

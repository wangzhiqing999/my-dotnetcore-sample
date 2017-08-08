using Microsoft.EntityFrameworkCore;

namespace W1001_ABP_With_Zero.EntityFrameworkCore
{
    public static class W1001_ABP_With_ZeroDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<W1001_ABP_With_ZeroDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }
    }
}
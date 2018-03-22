using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace W1010_ABP_NetCode2.EntityFrameworkCore
{
    public static class W1010_ABP_NetCode2DbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<W1010_ABP_NetCode2DbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<W1010_ABP_NetCode2DbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}

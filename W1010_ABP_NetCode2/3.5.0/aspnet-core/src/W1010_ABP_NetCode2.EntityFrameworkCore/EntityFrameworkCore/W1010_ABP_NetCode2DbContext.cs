using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using W1010_ABP_NetCode2.Authorization.Roles;
using W1010_ABP_NetCode2.Authorization.Users;
using W1010_ABP_NetCode2.MultiTenancy;


using W1010_ABP_NetCode2.Tasks;


namespace W1010_ABP_NetCode2.EntityFrameworkCore
{
    public class W1010_ABP_NetCode2DbContext : AbpZeroDbContext<Tenant, Role, User, W1010_ABP_NetCode2DbContext>
    {
        /* Define a DbSet for each entity of the application */

        public DbSet<Task> Tasks { get; set; }


        public DbSet<Other> Others { get; set; }



        public DbSet<TestUserType> TestUserTypes { get; set; }




        public W1010_ABP_NetCode2DbContext(DbContextOptions<W1010_ABP_NetCode2DbContext> options)
            : base(options)
        {
        }
    }
}

﻿using Abp.Zero.EntityFrameworkCore;
using W1001_ABP_With_Zero.Authorization.Roles;
using W1001_ABP_With_Zero.Authorization.Users;
using W1001_ABP_With_Zero.MultiTenancy;
using Microsoft.EntityFrameworkCore;

using W1001_ABP_With_Zero.Tasks;



namespace W1001_ABP_With_Zero.EntityFrameworkCore
{
    public class W1001_ABP_With_ZeroDbContext : AbpZeroDbContext<Tenant, Role, User, W1001_ABP_With_ZeroDbContext>
    {
        /* Define an IDbSet for each entity of the application */
        public DbSet<Task> Tasks { get; set; }


        public DbSet<Other> Others { get; set; }



        public DbSet<TestUserType> TestUserTypes { get; set; }





        public W1001_ABP_With_ZeroDbContext(DbContextOptions<W1001_ABP_With_ZeroDbContext> options)
            : base(options)
        {

        }
    }
}

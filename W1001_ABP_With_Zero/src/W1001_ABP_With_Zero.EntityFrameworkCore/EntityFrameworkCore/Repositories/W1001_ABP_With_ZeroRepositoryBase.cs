using Abp.Domain.Entities;
using Abp.EntityFrameworkCore;
using Abp.EntityFrameworkCore.Repositories;

namespace W1001_ABP_With_Zero.EntityFrameworkCore.Repositories
{
    /// <summary>
    /// Base class for custom repositories of the application.
    /// </summary>
    /// <typeparam name="TEntity">Entity type</typeparam>
    /// <typeparam name="TPrimaryKey">Primary key type of the entity</typeparam>
    public abstract class W1001_ABP_With_ZeroRepositoryBase<TEntity, TPrimaryKey> : EfCoreRepositoryBase<W1001_ABP_With_ZeroDbContext, TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        protected W1001_ABP_With_ZeroRepositoryBase(IDbContextProvider<W1001_ABP_With_ZeroDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        //add your common methods for all repositories
    }

    /// <summary>
    /// Base class for custom repositories of the application.
    /// This is a shortcut of <see cref="W1001_ABP_With_ZeroRepositoryBase{TEntity,TPrimaryKey}"/> for <see cref="int"/> primary key.
    /// </summary>
    /// <typeparam name="TEntity">Entity type</typeparam>
    public abstract class W1001_ABP_With_ZeroRepositoryBase<TEntity> : W1001_ABP_With_ZeroRepositoryBase<TEntity, int>
        where TEntity : class, IEntity<int>
    {
        protected W1001_ABP_With_ZeroRepositoryBase(IDbContextProvider<W1001_ABP_With_ZeroDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        //do not add any method here, add to the class above (since this inherits it)!!!
    }
}

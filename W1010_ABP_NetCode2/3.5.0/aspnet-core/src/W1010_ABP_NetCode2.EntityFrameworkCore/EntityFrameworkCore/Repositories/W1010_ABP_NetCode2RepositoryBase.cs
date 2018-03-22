using Abp.Domain.Entities;
using Abp.EntityFrameworkCore;
using Abp.EntityFrameworkCore.Repositories;

namespace W1010_ABP_NetCode2.EntityFrameworkCore.Repositories
{
    /// <summary>
    /// Base class for custom repositories of the application.
    /// </summary>
    /// <typeparam name="TEntity">Entity type</typeparam>
    /// <typeparam name="TPrimaryKey">Primary key type of the entity</typeparam>
    public abstract class W1010_ABP_NetCode2RepositoryBase<TEntity, TPrimaryKey> : EfCoreRepositoryBase<W1010_ABP_NetCode2DbContext, TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        protected W1010_ABP_NetCode2RepositoryBase(IDbContextProvider<W1010_ABP_NetCode2DbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        // Add your common methods for all repositories
    }

    /// <summary>
    /// Base class for custom repositories of the application.
    /// This is a shortcut of <see cref="W1010_ABP_NetCode2RepositoryBase{TEntity,TPrimaryKey}"/> for <see cref="int"/> primary key.
    /// </summary>
    /// <typeparam name="TEntity">Entity type</typeparam>
    public abstract class W1010_ABP_NetCode2RepositoryBase<TEntity> : W1010_ABP_NetCode2RepositoryBase<TEntity, int>
        where TEntity : class, IEntity<int>
    {
        protected W1010_ABP_NetCode2RepositoryBase(IDbContextProvider<W1010_ABP_NetCode2DbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        // Do not add any method here, add to the class above (since this inherits it)!!!
    }
}

using Microsoft.EntityFrameworkCore;
using PoC.Data.AbstractProducts.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PoC.Data.ConcreteProducts.Repositories
{
    public class BaseRepository<TEntity, TDbContext> : IBaseRepository<TEntity>
       where TEntity : class
       where TDbContext : DbContext
    {
        protected readonly DbSet<TEntity> DbSet;
        protected readonly TDbContext DbContext;

        public BaseRepository(TDbContext dbContext)
        {
            DbContext = dbContext;
            DbSet = DbContext.Set<TEntity>();
        }

        public virtual TEntity Get(object id)
        {
            return DbSet.Find(id);
        }

        public virtual Task<TEntity> GetAsync(object id, CancellationToken cancellationToken = default(CancellationToken))
        {
            return DbSet.FindAsync(new[] { id }, cancellationToken).AsTask();
        }

        public virtual IList<TEntity> GetAll()
        {
            return DbSet.ToList();
        }

        public virtual async Task<IList<TEntity>> GetAllAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return await DbSet.ToListAsync(cancellationToken);
        }

        public virtual void Add(TEntity entity)
        {
            DbContext.Add(entity);
        }

        public virtual void Update(TEntity entity)
        {
            DbContext.Update(entity);
        }

        public virtual void Remove(TEntity entity)
        {
            DbContext.Remove(entity);
        }
        public virtual void Detach(TEntity entity)
        {
            DbContext.Entry(entity).State = EntityState.Detached;
        }
    }
}

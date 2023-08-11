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
    public class QueryRepository<TEntity, TDbContext> : IQueryRepository<TEntity>
        where TEntity : class
        where TDbContext : DbContext
    {
        protected readonly DbSet<TEntity> DbQuery;
        protected readonly TDbContext DbContext;

        public QueryRepository(TDbContext dbContext)
        {
            DbContext = dbContext;
            DbQuery = DbContext.Set<TEntity>();
        }

        public virtual IList<TEntity> GetAll()
        {
            return DbQuery.ToList();
        }

        public virtual async Task<IList<TEntity>> GetAllAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return await DbQuery.ToListAsync(cancellationToken);
        }
    }
}

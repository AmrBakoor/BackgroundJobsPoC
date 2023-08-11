using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PoC.Data.AbstractProducts.Repository
{
    public interface IBaseRepository<TEntity> : IQueryRepository<TEntity> where TEntity : class
    {
        TEntity Get(object id);
        Task<TEntity> GetAsync(object id, CancellationToken cancellationToken = default(CancellationToken));

        void Add(TEntity entity);
        void Update(TEntity entity);
        void Remove(TEntity entity);
        void Detach(TEntity entity);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PoC.Data.AbstractProducts.Repository
{
    public interface IQueryRepository<TEntity> where TEntity : class
    {
        IList<TEntity> GetAll();
        Task<IList<TEntity>> GetAllAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}

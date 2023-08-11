using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PoC.Data.AbstractProducts.UnitOfWork
{
    public interface IBaseUnitOfWork
    {
        void SaveChanges();
        Task SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
        TRepository GetRepository<TRepository>() where TRepository : class;
    }
}


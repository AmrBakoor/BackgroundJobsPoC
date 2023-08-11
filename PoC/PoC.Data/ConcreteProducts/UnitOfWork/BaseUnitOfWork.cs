using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PoC.Data.AbstractProducts.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PoC.Data.ConcreteProducts
{
    public class BaseUnitOfWork<TDbContext> : IBaseUnitOfWork
       where TDbContext : DbContext
    {
        private readonly TDbContext _dbContext;
        private readonly IServiceProvider _serviceProvider;
        private readonly Dictionary<Type, object> _repositories = new Dictionary<Type, object>();

        public TDbContext DbContext => _dbContext;


        public BaseUnitOfWork(TDbContext dbContext, IServiceProvider serviceProvider)
        {
            _dbContext = dbContext;
            _serviceProvider = serviceProvider;
        }

        public void Dispose()
        {
            DbContext.Dispose();
            foreach (var repository in _repositories.Values.Select(r => r as IDisposable).Where(r => r != null))
            {
                repository.Dispose();
            }
        }

        public TRepository GetRepository<TRepository>()
            where TRepository : class
        {
            if (!_repositories.TryGetValue(typeof(TRepository), out var repository))
            {
                var repositoryFactory = _serviceProvider.GetRequiredService<Func<TDbContext, TRepository>>();
                repository = repositoryFactory(_dbContext);
                _repositories[typeof(TRepository)] = repository;
            }
            return (TRepository)repository;
        }

        public Task SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return DbContext.SaveChangesAsync(cancellationToken);
        }

        public void SaveChanges()
        {
            DbContext.SaveChanges();
        }
    }
}

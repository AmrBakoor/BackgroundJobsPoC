using System;
using Microsoft.EntityFrameworkCore;
using PoC.Data.AbstractProducts;
using PoC.Data.AbstractProducts.UnitOfWork;
using PoC.Data.ConcreteProducts;
using PoC.Data.DependencyInjection;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class UnitOfWorkServiceCollectionExtensions
    {
        public static IServiceCollection AddUnitOfWork<TDbContext>(this IServiceCollection serviceCollection, Action<RepositoryConfiguration<TDbContext, IBaseUnitOfWork>> repositoryConfiguration)
            where TDbContext : DbContext
        {
            return serviceCollection.AddUnitOfWork<TDbContext, IBaseUnitOfWork, BaseUnitOfWork<TDbContext>>(repositoryConfiguration);
        }

        public static IServiceCollection AddUnitOfWork<TDbContext, TUnitOfWorkService, TUnitOfWorkImplementation>(this IServiceCollection serviceCollection, Action<RepositoryConfiguration<TDbContext, TUnitOfWorkService>> repositoryConfiguration, ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
            where TDbContext : DbContext
            where TUnitOfWorkService : class, IBaseUnitOfWork
            where TUnitOfWorkImplementation : BaseUnitOfWork<TDbContext>, TUnitOfWorkService
        {
            serviceCollection.Add(new ServiceDescriptor(typeof(TUnitOfWorkService), typeof(TUnitOfWorkImplementation), serviceLifetime));

            var repoConfig = new RepositoryConfiguration<TDbContext, TUnitOfWorkService>(serviceCollection);
            repositoryConfiguration(repoConfig);

            return serviceCollection;
        }
    }
}

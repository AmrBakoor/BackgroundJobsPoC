using System;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using PoC.Data.AbstractProducts.UnitOfWork;

namespace PoC.Data.DependencyInjection
{
    public class RepositoryConfiguration<TDbContext, TUnitOfWorkService>
        where TUnitOfWorkService : IBaseUnitOfWork
    {
        private readonly IServiceCollection _serviceCollection;

        public RepositoryConfiguration(IServiceCollection serviceCollection)
        {
            _serviceCollection = serviceCollection;
        }

      
        public RepositoryConfiguration<TDbContext, TUnitOfWorkService> AddRepository<TService, TImplementation>()
            where TService : class
            where TImplementation : TService
        {
            
            _serviceCollection.AddTransient<Func<TDbContext, TService>>(serviceProvider => dbContext => ActivatorUtilities.CreateInstance<TImplementation>(serviceProvider, dbContext));

           
            _serviceCollection.AddTransient<TService>(serviceProvider => serviceProvider.GetRequiredService<TUnitOfWorkService>().GetRepository<TService>());
            return this;
        }

        public RepositoryConfiguration<TDbContext, TUnitOfWorkService> AddRepositoriesFromAssemblyOf<TAssemblySelector>()
        {
            var assembly = typeof(TAssemblySelector).GetTypeInfo().Assembly;
            var assemblyTypes = assembly.DefinedTypes.Select(ti => ti.AsType());

            var repoDescriptions = assemblyTypes
                .Select(t => t.GetTypeInfo())
                .Where(t => t.IsClass && !t.IsAbstract && t.Name.EndsWith("Repository"))
                .Select(t =>
                {
                    var matchingInterface = t.FindInterfaces((it, c) => it.Name == $"I{c}", t.Name).FirstOrDefault();
                    if (matchingInterface == null)
                        return null;
                    return new { Type = t.AsType(), InterfaceType = matchingInterface };
                })
                .Where(x => x != null);

            var addRepositoryMethod = GetType().GetMethod(nameof(AddRepository));

            foreach (var repoDescription in repoDescriptions)
            {
                var constructedMethod = addRepositoryMethod.MakeGenericMethod(repoDescription.InterfaceType, repoDescription.Type);
                constructedMethod.Invoke(this, new object[] { });
            }
            return this;
        }
    }
}

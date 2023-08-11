using System;
using System.Reflection;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PoC.Data.Core;
using Microsoft.Extensions.Configuration;
using PoC.BL.AbstractProducts;
using PoC.BL.ConcreteProducts;
using PoC.Client.Console.Sql.AbstractProducts;
using PoC.Client.Console.Sql.ConcreteProducts;
using PoC.Data.AbstractProducts.UnitOfWork;
using PoC.Data.ConcreteProducts.UnitOfWork;

namespace PoC.Client.Console.Sql.IoC
{
	public static class IoC
	{
        public static IServiceProvider ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<PoCDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("PoCDbContext"));
            }, ServiceLifetime.Transient);

            services.AddUnitOfWork<PoCDbContext, IPoCUnitOfWork, PoCUnitOfWork>(repoConfig =>
               repoConfig.AddRepositoriesFromAssemblyOf<PoCUnitOfWork>());

            services.AddScoped<IConsoleHandler, ConsoleHandler>();
            services.AddScoped<IBusinessProcessor, BusinessProcessor>();
        
            var containerBuilder = new ContainerBuilder();

            return ConfigureIoC(services, containerBuilder);
        }
        public static IServiceProvider ConfigureIoC(IServiceCollection services, ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterAssemblyTypes(Assembly.Load("PoC.Data")).AsImplementedInterfaces().InstancePerLifetimeScope();
            containerBuilder.RegisterAssemblyTypes(Assembly.Load("PoC.BL")).AsImplementedInterfaces().InstancePerLifetimeScope();
            containerBuilder.Populate(services);
            var container = containerBuilder.Build();
            return new AutofacServiceProvider(container);
        }
    }
}


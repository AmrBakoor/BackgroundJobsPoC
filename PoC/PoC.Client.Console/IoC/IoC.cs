using System;
using System.Reflection;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using PoC.BL.AbstractProducts;
using PoC.BL.ConcreteProducts;
using PoC.Client.Console.AbstractProducts;
using PoC.Client.Console.ConcreteProducts;

namespace PoC.Client.Console.IoC
{
	public static class IoC
	{
        public static IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IConsoleHandler, ConsoleHandler>();
            services.AddScoped<IBusinessProcessor, BusinessProcessor>();
            var containerBuilder = new ContainerBuilder();
            return ConfigureIoC(services, containerBuilder);
        }
        public static IServiceProvider ConfigureIoC(IServiceCollection services, ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterAssemblyTypes(Assembly.Load("PoC.BL")).AsImplementedInterfaces().InstancePerLifetimeScope();
            containerBuilder.Populate(services);
            var container = containerBuilder.Build();
            return new AutofacServiceProvider(container);
        }
    }
}


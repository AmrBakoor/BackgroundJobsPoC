using System;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using PoC.Client.Console.AbstractProducts;
using PoC.Client.Console.ConcreteProducts;
using PoC.DomainEntities;

namespace PoC.Client.Console
{
    static class Program
    {
        static void Main(string[] args)
        {
           
            IBusinessProcessor businessProcessor = GetBusinessProcessor();
            var person = businessProcessor.PrepareInput();
            var data = businessProcessor.PrepareData(1, 100);
            var consoleMessages = businessProcessor.ProcessData(data, person);
            businessProcessor.PrintData(consoleMessages);
            
        }


        #region Private Behaviour
        private static IBusinessProcessor GetBusinessProcessor()
        {
            var services = new ServiceCollection();

            var serviceProvider = IoC.IoC.ConfigureServices(services);

            var businessProcessor = serviceProvider.GetRequiredService<IBusinessProcessor>();

            return businessProcessor;
        }
        #endregion

    }
}


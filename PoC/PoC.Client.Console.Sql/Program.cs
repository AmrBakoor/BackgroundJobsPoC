
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PoC.Client.Console.Sql.AbstractProducts;
using System;

namespace PoC.Client.Console.Sql
{
    internal class Program
    {
        public static IConfiguration Configuration { get; set; }

        static void Main(string[] args)
        {
            var configuration = BuildConfiguration();
            var businessProcessor = GetBusinessProcessor(configuration);
            var person = businessProcessor.PrepareInput();
            var data = businessProcessor.PrepareData(person);
            var consoleMessages = businessProcessor.ProcessData(data, person);
            businessProcessor.PrintData(consoleMessages);
        }

        #region Private Behaviour
        private static IConfiguration BuildConfiguration()
        {
            var builder = new ConfigurationBuilder();
            builder.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            return builder.Build();
        }
        private static IBusinessProcessor GetBusinessProcessor(IConfiguration configuration)
        {
            var services = new ServiceCollection();

            var serviceProvider = IoC.IoC.ConfigureServices(services, configuration);

            var businessProcessor = serviceProvider.GetRequiredService<IBusinessProcessor>();

            return businessProcessor;
        }
        #endregion
    }
}

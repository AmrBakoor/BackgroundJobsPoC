using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PoC.BL;
using PoC.Data.AbstractProducts.UnitOfWork;
using PoC.Data.ConcreteProducts.UnitOfWork;
using PoC.Data.Core;
using PoC.Runner.Jobs;
using Quartz;
using Quartz.Impl;
using Quartz.Simpl;
using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace PoC.Runner
{
    internal class Program
    {
        #region Public Memebers
        public static IContainer Container;
        public static IConfiguration Configuration { get; set; }
        #endregion

        static async Task Main(string[] args)
        {
            Configuration = BuildConfiguration();
            var services = new ServiceCollection();
            var serviceProvider = ConfigureServices(services, Configuration);
            var scheduler = await ConfigureScheduler(serviceProvider);
            await scheduler.Start();
            RegisterCancelEvent(scheduler);
            await ExitSemaphore.WaitAsync();       
            await scheduler.PauseAll();
            var executingJobs = await scheduler.GetCurrentlyExecutingJobs();
            foreach (var job in executingJobs)
            {          
                await scheduler.Interrupt(job.FireInstanceId);
            }
            await scheduler.Shutdown(true);         
        }

        #region Private Behaviour

        private static SemaphoreSlim ExitSemaphore = new SemaphoreSlim(0);

        private static IConfiguration BuildConfiguration()
        {
            var builder = new ConfigurationBuilder();
            builder.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            return builder.Build();
        }

      

        private static IServiceProvider ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<PoCDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("PoCDbContext"));
            }, ServiceLifetime.Transient);

            services.AddUnitOfWork<PoCDbContext, IPoCUnitOfWork, PoCUnitOfWork>(repoConfig =>
                repoConfig.AddRepositoriesFromAssemblyOf<PoCUnitOfWork>());

            services.Configure<QuartzOptions>(options => { });

            services.AddAutoMapper(typeof(AutoMapperConfig).Assembly);

            var containerBuilder = new ContainerBuilder();

            return ConfigureIoC(services, configuration, containerBuilder);
        }

        private static IServiceProvider ConfigureIoC(IServiceCollection services, IConfiguration configuration, ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType<MicrosoftDependencyInjectionJobFactory>().AsSelf().InstancePerDependency();
            containerBuilder.RegisterAssemblyTypes(Assembly.Load("PoC.Data")).AsImplementedInterfaces().InstancePerLifetimeScope();
            containerBuilder.RegisterAssemblyTypes(Assembly.Load("PoC.BL")).AsImplementedInterfaces().InstancePerLifetimeScope();
            containerBuilder.Populate(services);
            Container = containerBuilder.Build();
            return new AutofacServiceProvider(Container);
        }

        private async static Task<IScheduler> ConfigureScheduler(IServiceProvider serviceProvider)
        {
            var scheduler = await StdSchedulerFactory.GetDefaultScheduler();
            scheduler.JobFactory = serviceProvider.GetRequiredService<MicrosoftDependencyInjectionJobFactory>();

            var batchJobsJob = JobBuilder.Create<CalculationJob>()
                .WithIdentity("calculationJob")
                .Build();

            var batchJobsTrigger = TriggerBuilder.Create()
                .WithIdentity("calculationJobTrigger")
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInMinutes(1)
                    .RepeatForever())
                .Build();

            await scheduler.ScheduleJob(batchJobsJob, batchJobsTrigger);

            return scheduler;
        }

        private static void RegisterCancelEvent(IScheduler scheduler)
        {
            Console.CancelKeyPress += (sender, args) =>
            {
                ExitSemaphore.Release();
                args.Cancel = true;
            };
        }

        #endregion
    }
}

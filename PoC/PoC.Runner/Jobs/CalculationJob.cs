using Autofac;
using PoC.BL.AbstractProducts;
using PoC.Data.AbstractProducts.UnitOfWork;
using PoC.DomainEntities.Jobs;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoC.Runner.Jobs
{
    public class CalculationJob : IJob
    {
        private readonly IPoCUnitOfWork _poCUnitOfWork;
        private readonly IJobProcessor  _jobProcessor;
        private readonly ILifetimeScope _lifetimeScope;
        private readonly IJobService _jobService;

        public CalculationJob(IPoCUnitOfWork poCUnitOfWork, IJobProcessor jobProcessor, IJobService jobService, ILifetimeScope  lifetimeScope)
        {
            _jobProcessor = jobProcessor;
            _poCUnitOfWork = poCUnitOfWork;
            _jobService = jobService;
            _lifetimeScope = lifetimeScope;
         }


        public async Task Execute(IJobExecutionContext context)
        {
           
            var jobs = await _jobService.GetNewJobsAndMarkStartedAsync();
           
            var tasks = jobs.Select(job => ExecuteJob(job));

            await Task.WhenAll(tasks);
        }
        private async Task ExecuteJob(Job job)
        {
            var processor = _lifetimeScope.Resolve<IJobProcessor>();
            bool hasErrors;
            try
            {
                hasErrors = await processor.ExecuteAsync(job.JobId);
             
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}

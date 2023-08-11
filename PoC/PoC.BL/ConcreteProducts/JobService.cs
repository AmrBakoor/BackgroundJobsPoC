using Autofac;
using AutoMapper;
using PoC.BL.AbstractProducts;
using PoC.Data.AbstractProducts.Repository;
using PoC.Data.AbstractProducts.UnitOfWork;
using PoC.DomainEntities;
using PoC.DomainEntities.Jobs;
using PoC.Dtos;
using PoC.Dtos.Dtos;
using PoC.Dtos.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PoC.BL.ConcreteProducts
{
    public class JobService : IJobService
    {
        private readonly IPoCUnitOfWork _poCUnitOfWork;
        private readonly IMapper _mapper;
        protected static readonly SemaphoreSlim semaphoreSlim = new SemaphoreSlim(1);
        public JobService(IPoCUnitOfWork poCUnitOfWork ,IMapper mapper)
        {
            _poCUnitOfWork = poCUnitOfWork;
            _mapper = mapper;
        }

        public async Task<Guid> PostJob(PostJobRequestDto jobDto)
        {
            var job = new Job()
            {
                JobId = Guid.NewGuid(),
                CreatedAt = DateTime.UtcNow,
                StatusEnum = JobStatusEnum.PENDING,
                FirstName = jobDto.FirstName,
                LastName = jobDto.LastName
                
            };
            _poCUnitOfWork.JobRepository.Add(job);
            await _poCUnitOfWork.SaveChangesAsync();
            return job.JobId;
        }

        public async Task<ICollection<Job>> GetNewJobsAndMarkStartedAsync()
        {
            await semaphoreSlim.WaitAsync();
            try
            {
                var jobs = await _poCUnitOfWork.JobRepository.GetByStatusAsync(JobStatusEnum.PENDING);
                foreach (var job in jobs)
                {
                    job.StatusEnum = JobStatusEnum.STARTED;
                    job.StartedAt = DateTime.UtcNow;
                }
                await _poCUnitOfWork.SaveChangesAsync();
                return jobs;
            }
            finally
            {
                semaphoreSlim.Release();
            }
        }

        public async Task<StatusObjectResponseDto> GetStatus(Guid jobId)
        {
            var job = await _poCUnitOfWork.JobRepository.GetByJobIdAsync(jobId);
            return _mapper.Map<Job, StatusObjectResponseDto>(job);
        }

        public async Task<List<JobResponseDto>> GetAllAsync()
        {
            var jobs = await _poCUnitOfWork.JobRepository.GetAllAsync();
            return jobs.Select(_mapper.Map<Job, JobResponseDto>).ToList();
        }
    }
}

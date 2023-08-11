using Microsoft.EntityFrameworkCore;
using PoC.Data.AbstractProducts.Repository;
using PoC.Data.ConcreteProducts.Repositories;
using PoC.Data.Core;
using PoC.DomainEntities.Jobs;
using PoC.Dtos.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoC.Data.ConcreteProducts.Repository
{
    public class JobRepository: BaseRepository<Job, PoCDbContext>, IJobRepository
    {
        public JobRepository(PoCDbContext dbContext) : base(dbContext)
        {

        }
        public async Task<Job> GetByJobIdAsync(Guid jobId)
        {
            return  await DbSet.SingleOrDefaultAsync(x => x.JobId == jobId);
        }
        public async Task<ICollection<Job>> GetByStatusAsync(JobStatusEnum batchJobStatus)
        {
            return await DbSet.Where(x => x.StatusId == (int)batchJobStatus).ToListAsync();
        }
    }
}

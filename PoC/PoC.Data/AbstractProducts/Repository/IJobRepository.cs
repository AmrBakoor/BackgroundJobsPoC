using PoC.DomainEntities.Jobs;
using PoC.Dtos.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoC.Data.AbstractProducts.Repository
{
    public interface IJobRepository: IBaseRepository<Job>
    {
        Task<Job> GetByJobIdAsync(Guid jobId);
        Task<ICollection<Job>> GetByStatusAsync(JobStatusEnum batchJobStatus);
    }
}

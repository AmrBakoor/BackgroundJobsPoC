using PoC.DomainEntities;
using PoC.DomainEntities.Jobs;
using PoC.Dtos;
using PoC.Dtos.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoC.BL.AbstractProducts
{
    public interface IJobService
    {
        Task<Guid> PostJob(PostJobRequestDto job);
        Task<ICollection<Job>> GetNewJobsAndMarkStartedAsync();
        Task<StatusObjectResponseDto> GetStatus(Guid jobId);
        Task<List<JobResponseDto>> GetAllAsync();
    }
}

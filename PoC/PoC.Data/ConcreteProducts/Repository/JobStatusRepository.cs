using PoC.Data.AbstractProducts.Repository;
using PoC.Data.ConcreteProducts.Repositories;
using PoC.Data.Core;
using PoC.DomainEntities.Jobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoC.Data.ConcreteProducts.Repository
{
    public class JobStatusRepository : BaseRepository<JobStatus, PoCDbContext>, IJobStatusRepository
    {
        public JobStatusRepository(PoCDbContext dbContext) : base(dbContext)
        {

        }
    }
}

using PoC.Data.AbstractProducts.Repository;
using PoC.Data.AbstractProducts.UnitOfWork;
using PoC.Data.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoC.Data.ConcreteProducts.UnitOfWork
{
    public class PoCUnitOfWork : BaseUnitOfWork<PoCDbContext>, IPoCUnitOfWork
    {
        public PoCUnitOfWork(PoCDbContext dbContext, IServiceProvider serviceProvider) : base(dbContext, serviceProvider)
        {

        }
        public IJobStatusRepository JobStatusRepository => GetRepository<IJobStatusRepository>();

        public IJobRepository JobRepository => GetRepository<IJobRepository>();
    }
}

using PoC.Data.AbstractProducts.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoC.Data.AbstractProducts.UnitOfWork
{
    public interface IPoCUnitOfWork: IBaseUnitOfWork
    {
        IJobStatusRepository JobStatusRepository { get; }

        IJobRepository JobRepository { get; }
    }
}

using PoC.DomainEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoC.Data.AbstractProducts.Repository
{
    public interface IOutpuRepository: IBaseRepository<Output>
    {
        void ClearData();
        void PopulateData(Person person);
        IQueryable<Output> Get();
    }
}

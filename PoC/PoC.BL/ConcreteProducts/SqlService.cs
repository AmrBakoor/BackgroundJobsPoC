using PoC.BL.AbstractProducts;
using PoC.Data.AbstractProducts.Repository;
using PoC.DomainEntities;
using System.Linq;

namespace PoC.BL.ConcreteProducts
{
    public class SqlService : ISqlService
    {
        private readonly IOutpuRepository _outpuRepository;
        public SqlService(IOutpuRepository outpuRepository)
        {
            _outpuRepository = outpuRepository;
        }

        public void PopulateData(Person person)
        {
            _outpuRepository.PopulateData(person);

        }
        public IQueryable<Output> Get()
        {
          return _outpuRepository.Get();
           
        }

        public void ClearData()
        {
            _outpuRepository.ClearData();
        }


    }
}

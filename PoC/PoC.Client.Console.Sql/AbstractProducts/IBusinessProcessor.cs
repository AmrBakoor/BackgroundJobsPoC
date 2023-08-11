using PoC.BL.AbstractProducts;
using PoC.DomainEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoC.Client.Console.Sql.AbstractProducts
{
    public interface IBusinessProcessor: IBusinessProcessorBase
    {
        Dictionary<int, string> ProcessData(IQueryable<Output> data, Person person);

        void PrintData(Dictionary<int, string> data);

        IQueryable<Output> PrepareData(Person person);
    }
}

using PoC.DomainEntities;
using System.Linq;



namespace PoC.BL.AbstractProducts
{
    public interface ISqlService
    {
        void PopulateData(Person person);
        IQueryable<Output> Get();

        void ClearData();
    }
}

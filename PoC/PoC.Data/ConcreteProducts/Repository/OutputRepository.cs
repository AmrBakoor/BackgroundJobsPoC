using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PoC.Data.AbstractProducts.Repository;
using PoC.Data.ConcreteProducts.Repositories;
using PoC.Data.Core;
using PoC.DomainEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoC.Data.ConcreteProducts.Repository
{
    public class OutputRepository : BaseRepository<Output, PoCDbContext>, IOutpuRepository
    {
        public OutputRepository(PoCDbContext dbContext) : base(dbContext)
        {
        }

        public void ClearData()
        {
            if (DbSet.Any())
                DbContext.Database.ExecuteSqlRaw("exec dbo.ClearData");
        }

        public IQueryable<Output> Get()
        {
            return DbContext.Outputs.AsQueryable();
        }

        public void PopulateData(Person person)
        {
            List<SqlParameter> sqlParameters = new List<SqlParameter>()
            {
                new SqlParameter("@firstName", person.FirstName),
                new SqlParameter("@lastName", person.LastName)
            };
            DbContext.Database.ExecuteSqlRaw($"exec dbo.PopulateData @firstName, @lastName", sqlParameters[0], sqlParameters[1]);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PoC.Data.Core
{
    public class BaseDbContext: DbContext
    {
        public BaseDbContext(DbContextOptions options) : base(options)
        { }

        public BaseDbContext()
        { }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            
            try
            {
                return base.SaveChanges(acceptAllChangesOnSuccess);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}

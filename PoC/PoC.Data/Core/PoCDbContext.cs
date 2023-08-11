using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using PoC.DomainEntities;
using PoC.DomainEntities.Jobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoC.Data.Core
{
    public class PoCDbContext: BaseDbContext
    {
        public virtual DbSet<Output> Outputs { get; set; }

        public virtual DbSet<Job> Jobs { get; set; }

        public virtual DbSet<JobStatus> JobStatuses { get; set; }

        public PoCDbContext(DbContextOptions<PoCDbContext> options) : base(options)
        {
           
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }
    }
}

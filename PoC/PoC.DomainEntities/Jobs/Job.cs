using PoC.Dtos.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoC.DomainEntities.Jobs
{
    [Table("Jobs")]
    public class Job
    {
        public int Id { get; set; }

        public Guid JobId { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? StartedAt { get; set; }

        public DateTime? CompletedAt { get; set; }

        public int StatusId { get; set; }

        [ForeignKey("StatusId")]
        public virtual JobStatus Status { get; set; }

        public bool HasErros { get; set; }

        public string Result { get; set; }

        public int Progress { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        [NotMapped]
        public JobStatusEnum StatusEnum
        {
            get => (JobStatusEnum)StatusId;
            set => StatusId = (int)value;
        }
    }
}

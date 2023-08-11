using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoC.Dtos.Dtos
{
    public class JobResponseDto
    {
      
        public Guid JobId { get; set; }

        public string Status { get; set; }

        public string StartedAt { get; set; }

        public string CompletedAt { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Progress { get; set; }

    }
}

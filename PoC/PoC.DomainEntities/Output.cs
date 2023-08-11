using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoC.DomainEntities
{
    [Table("Output")]
    public class Output
    {
        public int Id { get; set; }
        public int Number { get;set; }
        public bool IsDivisibleByThree { get; set; }
        public bool IsDivisibleByFive { get; set; }
        public bool IsDivisibleByThreeAndFive { get; set; }
        public bool IsNeutral { get; set; }
    }
}

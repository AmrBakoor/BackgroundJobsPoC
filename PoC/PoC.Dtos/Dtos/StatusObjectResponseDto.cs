using PoC.Dtos.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoC.Dtos.Dtos
{
    public class StatusObjectResponseDto
    {
        public string Status { get; set; }
        public int Progress { get; set; }
        public string Result { get; set; }
    }
}

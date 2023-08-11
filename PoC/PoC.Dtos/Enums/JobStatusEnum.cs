using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoC.Dtos.Enums
{
    public enum JobStatusEnum
    {
        [Display(Name = "New")]
        PENDING = 1,
        [Display(Name = "In progress")]
        STARTED = 2,
        [Display(Name = "Success")]
        SUCCESS = 3,
        [Display(Name = "Failed")]
        FAILED = 4
    }
}

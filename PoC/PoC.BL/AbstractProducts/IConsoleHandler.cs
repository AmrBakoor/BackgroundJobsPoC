using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoC.BL.AbstractProducts
{
    public interface IConsoleHandler
    {
        void Print(string message);
        string Read();
    }
}

using System;
using System.Linq;
using PoC.BL.AbstractProducts;

namespace CleanCode.BL.ConcreteProducts
{
    public class NumberRangeGenerator : INumberRangeGenerator
    {
        public int[] GenerateArrayOfNumbers(int floor, int ceil)
        {
            if (floor >= ceil)
                throw new ArgumentException("floor can't be greater than or equal to ceil");
            
             return Enumerable.Range(floor, ceil).OrderBy(x => x).ToArray();

        }
    }
}


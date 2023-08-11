using System;
using PoC.BL.AbstractProducts;

namespace CleanCode.BL.ConcreteProducts
{
    public class NumberValidator : INumberValidator
    {
        public NumberState Validate(int num)
        {
            NumberState numberState = new();
           
            if (num % 3 == 0)
               numberState.DivisibleByThree = true;

            if (num % 5 == 0)
               numberState.DivisibleByFive = true;
                
            return numberState;
        }
    }
}


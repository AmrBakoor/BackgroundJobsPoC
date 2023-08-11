using System;
namespace PoC.BL.AbstractProducts
{
	public interface INumberValidator
	{
		NumberState Validate(int num);
	}

	public class NumberState
    {
		public bool DivisibleByThree { get; set; }

		public bool DivisibleByFive { get; set; }

		
		public static bool IsDivisibleByThreeAndFive(NumberState numberState)
        {
			return numberState.DivisibleByThree && numberState.DivisibleByFive;
        }

		public static bool IsNeutral(NumberState numberState)
		{
			return !numberState.DivisibleByThree && !numberState.DivisibleByFive;
		}
	}
}


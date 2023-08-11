using System;
namespace PoC.BL.AbstractProducts
{
	public interface INumberRangeGenerator
	{

		int[] GenerateArrayOfNumbers(int floor, int ceil);
	}
}


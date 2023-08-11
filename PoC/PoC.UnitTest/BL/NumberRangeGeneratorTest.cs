using System;
using System.Linq;
using CleanCode.BL.ConcreteProducts;
using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PoC.BL.AbstractProducts;

namespace PoC.UnitTest.BL
{
	[TestClass]
	public class NumberRangeGeneratorTest
	{

		[TestMethod]
		public void Generate_Array_Of_Numbers_With_Valid_Input_Should_Return_Array_Of_Numbers()
        {
			var numberRangeGenerator = new NumberRangeGenerator();
			var arrayOfOrderedNumbers = numberRangeGenerator.GenerateArrayOfNumbers(1, 100);
			Assert.IsTrue(arrayOfOrderedNumbers.Length > 0);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void Generate_Array_Of_Numbers_With_Invalid_Input_Should_Throw_Argument_Exception()
		{

			var numberRangeGenerator = new NumberRangeGenerator();
			var arrayOfOrderedNumbers = numberRangeGenerator.GenerateArrayOfNumbers(100, 100);
		
		}


	}
}


using System;
using CleanCode.BL.ConcreteProducts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PoC.BL.AbstractProducts;

namespace PoC.UnitTest.BL
{
	[TestClass]
	public class NumberValidatorTest
	{
		[TestMethod]
		public void Validate_With_Multiplier_Of_3_Should_Return_Divisible_By_3_Equals_To_True()
        {
			var numberValidator = new NumberValidator();
			var numberState = numberValidator.Validate(3);
			Assert.IsTrue(numberState.DivisibleByThree);
        }
		[TestMethod]
		public void Validate_With_Multiplier_Of_5_Should_Return_Divisible_By_5_Equals_To_True()
		{
			var numberValidator = new NumberValidator();
			var numberState = numberValidator.Validate(5);
			Assert.IsTrue(numberState.DivisibleByFive);
		}
		[TestMethod]
		public void Validate_With_Multiplier_Of_3_And_5_Should_Return_Divisible_By_3_And_5_Equals_To_True()
		{
			var numberValidator = new NumberValidator();
			var numberState = numberValidator.Validate(15);
			Assert.IsTrue(NumberState.IsDivisibleByThreeAndFive(numberState));
		}
		[TestMethod]
		public void Validate_With_Non_Multiplier_Of_3_And_5_Should_Return_Neutral_To_True()
		{
			var numberValidator = new NumberValidator();
			var numberState = numberValidator.Validate(67);
			Assert.IsTrue(NumberState.IsNeutral(numberState));
		}
	}
}


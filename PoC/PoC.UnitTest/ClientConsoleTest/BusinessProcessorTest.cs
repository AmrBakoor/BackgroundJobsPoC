using System;
using System.Linq;
using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PoC.BL.AbstractProducts;
using PoC.Client.Console.AbstractProducts;
using PoC.Client.Console.ConcreteProducts;
using PoC.DomainEntities;

namespace PoC.UnitTest.ClientConsoleTest
{
	[TestClass]
	public class BusinessProcessorTest
	{
		
        [TestMethod]
        public void Process_Data_With_Input_Between_1_And_100_Should_Return_Hash_Table_Contains_6_Multipliers_Of_3_And_5()
        {
            Person person = new() { FirstName = "Amr", LastName = "Bakoor" };
            var arrayOfOrderedNumbers = Enumerable.Range(1, 100).OrderBy(x => x).ToArray();
            var fakeNumberRangeGenerator = A.Fake<INumberRangeGenerator>();
            A.CallTo(() => fakeNumberRangeGenerator.GenerateArrayOfNumbers(1, 100)).Returns(arrayOfOrderedNumbers);
            var fakeNumberValidator = A.Fake<INumberValidator>();
            A.CallTo(() => fakeNumberValidator.Validate(1)).WhenArgumentsMatch((int x) => x % 3 == 0 && x % 5 == 0).Returns(new NumberState() { DivisibleByFive = true, DivisibleByThree = true });
            var fakeConsoleHandler = A.Fake<IConsoleHandler>();
            var businessProcessor = new BusinessProcessor(fakeNumberRangeGenerator, fakeNumberValidator, fakeConsoleHandler);

            var consoleMessages = businessProcessor.ProcessData(arrayOfOrderedNumbers,person);
            Assert.IsTrue(consoleMessages.Where(x => x.Value.Equals("Amr Bakoor")).Count() == 6);
        }

        [TestMethod]
        public void Process_Data_With_Input_Between_1_And_100_Should_Return_Hash_Table_Contains_33_Multipliers_Of_3()
        {
            Person person = new() { FirstName = "Amr", LastName = "Bakoor" };
            var arrayOfOrderedNumbers = Enumerable.Range(1, 100).OrderBy(x => x).ToArray();
            var fakeNumberRangeGenerator = A.Fake<INumberRangeGenerator>();
            A.CallTo(() => fakeNumberRangeGenerator.GenerateArrayOfNumbers(1, 100)).Returns(arrayOfOrderedNumbers);
            var fakeNumberValidator = A.Fake<INumberValidator>();
            A.CallTo(() => fakeNumberValidator.Validate(1)).WhenArgumentsMatch((int x) => x % 3 == 0).Returns(new NumberState() { DivisibleByThree = true });
            var fakeConsoleHandler = A.Fake<IConsoleHandler>();
            var businessProcessor = new BusinessProcessor(fakeNumberRangeGenerator, fakeNumberValidator, fakeConsoleHandler);

            var consoleMessages = businessProcessor.ProcessData(arrayOfOrderedNumbers, person);
            Assert.IsTrue(consoleMessages.Where(x => x.Value.Equals("Amr")).Count() == 33);
        }

        [TestMethod]
        public void Process_Data_With_Input_Between_1_And_100_Should_Return_Hash_Table_Contains_20_Multipliers_Of_5()
        {
            Person person = new() { FirstName = "Amr", LastName = "Bakoor" };
            var arrayOfOrderedNumbers = Enumerable.Range(1, 100).OrderBy(x => x).ToArray();
            var fakeNumberRangeGenerator = A.Fake<INumberRangeGenerator>();
            A.CallTo(() => fakeNumberRangeGenerator.GenerateArrayOfNumbers(1, 100)).Returns(arrayOfOrderedNumbers);
            var fakeNumberValidator = A.Fake<INumberValidator>();
            A.CallTo(() => fakeNumberValidator.Validate(1)).WhenArgumentsMatch((int x) => x % 5 == 0).Returns(new NumberState() { DivisibleByFive = true });
            var fakeConsoleHandler = A.Fake<IConsoleHandler>();
            var businessProcessor = new BusinessProcessor(fakeNumberRangeGenerator, fakeNumberValidator, fakeConsoleHandler);

            var consoleMessages = businessProcessor.ProcessData(arrayOfOrderedNumbers, person);
            Assert.IsTrue(consoleMessages.Where(x => x.Value.Equals("Bakoor")).Count() == 20);
        }
    }
}


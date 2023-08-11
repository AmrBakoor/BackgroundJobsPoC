using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using PoC.BL.AbstractProducts;
using PoC.BL.ConcreteProducts;
using PoC.Client.Console.AbstractProducts;
using PoC.DomainEntities;

namespace PoC.Client.Console.ConcreteProducts
{
	public class BusinessProcessor : BusinessProcessorBase, IBusinessProcessor
    {
        private readonly INumberRangeGenerator _numberRangeGenerator;
        private readonly INumberValidator _numberValidator;
        private readonly IConsoleHandler _consoleHandler;

		public BusinessProcessor(INumberRangeGenerator numberRangeGenerator, INumberValidator numberValidator, IConsoleHandler consoleHandler) : base(consoleHandler)
		{
            _numberRangeGenerator = numberRangeGenerator;
            _numberValidator = numberValidator;
            _consoleHandler = consoleHandler;
        }




        #region Public Behaviour
        public int[] PrepareData(int floor, int ceil)
        {
            return _numberRangeGenerator.GenerateArrayOfNumbers(floor, ceil);
        }

        public void PrintData(Dictionary<int, string> data)
        {
            
            foreach (var item in data)
            {
                _consoleHandler.Print(item.Value);
            }
        }
            
        public Dictionary<int, string> ProcessData(int [] orderedNumbers, Person person)
        {
            Dictionary<int, string> consoleMessages = new();
           
            for (int i = 0; i < orderedNumbers.Length; i++)
            { 
                var numberState = _numberValidator.Validate(orderedNumbers[i]);
               
                if (numberState.DivisibleByThree && !numberState.DivisibleByFive)
                    consoleMessages.TryAdd(orderedNumbers[i], person.FirstName);

                if (numberState.DivisibleByFive && !numberState.DivisibleByThree)
                    consoleMessages.TryAdd(orderedNumbers[i], person.LastName);

                if (NumberState.IsDivisibleByThreeAndFive(numberState))
                    consoleMessages.TryAdd(orderedNumbers[i], person.FirstName + " " +  person.LastName);


                if (NumberState.IsNeutral(numberState))
                    consoleMessages.TryAdd(orderedNumbers[i], orderedNumbers[i].ToString());

            }

            return consoleMessages;
        }
        #endregion

    }
}


using PoC.BL.AbstractProducts;
using PoC.DomainEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoC.BL.ConcreteProducts
{
    public class BusinessProcessorBase : IBusinessProcessorBase
    {
        private readonly IConsoleHandler _consoleHandler;
        public BusinessProcessorBase(IConsoleHandler consoleHandler)
        {
            _consoleHandler = consoleHandler;
        }
        public Person PrepareInput()
        {
            Person person = new();
            _consoleHandler.Print("Welcome to PoC");
            _consoleHandler.Print("Please enter a valid first name.");
            string firstName = _consoleHandler.Read();
            _consoleHandler.Print("Please enter a valid last name.");
            string lastName = _consoleHandler.Read();
            person.FirstName = firstName;
            person.LastName = lastName;
            return person;
        }
    }
}

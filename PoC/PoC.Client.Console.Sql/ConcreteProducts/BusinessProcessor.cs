using PoC.BL.AbstractProducts;
using PoC.BL.ConcreteProducts;
using PoC.Client.Console.Sql.AbstractProducts;
using PoC.Data.AbstractProducts;
using PoC.DomainEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoC.Client.Console.Sql.ConcreteProducts
{
    public class BusinessProcessor: BusinessProcessorBase, IBusinessProcessor
    {

        private readonly IConsoleHandler _consoleHandler;
        private readonly ISqlService _sqlService;

        public BusinessProcessor(IConsoleHandler consoleHandler, ISqlService sqlService) :  base(consoleHandler)
        {
            _sqlService = sqlService;
            _consoleHandler = consoleHandler;
        }

        #region Public Behaviour
        public IQueryable<Output> PrepareData(Person person)
        {
            ClearData();
            PopulateData(person);
            return Get();
        }

        public Dictionary<int, string> ProcessData(IQueryable<Output> data, Person person)
        {
            Dictionary<int, string> dic = new();
            foreach (var item in data)
            {
                if (item.IsDivisibleByThree && !item.IsDivisibleByFive)
                    dic[item.Number] = person.FirstName;

                if (item.IsDivisibleByFive && !item.IsDivisibleByThree)
                    dic[item.Number] = person.LastName;

                if (item.IsDivisibleByThreeAndFive)
                    dic[item.Number] = person.FirstName + " " + person.LastName;

                if (item.IsNeutral)
                    dic[item.Number] = item.Number.ToString();
            }
            return dic;
        }


        public void PrintData(Dictionary<int, string> data)
        {
            foreach (var item in data)
            {
                _consoleHandler.Print(item.Value);
            }
        }
        #endregion


        #region Private Behaviour
        private void ClearData()
        {
            _sqlService.ClearData();
        }

        private IQueryable<Output> Get()
        {
            return _sqlService.Get();
        }

        private void PopulateData(Person person)
        {
            _sqlService.PopulateData(person);
        }
        #endregion
    }
}

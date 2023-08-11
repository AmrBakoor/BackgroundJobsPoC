using System;
using System.Collections.Generic;
using PoC.BL.AbstractProducts;
using PoC.DomainEntities;

namespace PoC.Client.Console.AbstractProducts
{
	public interface IBusinessProcessor : IBusinessProcessorBase
	{
		Dictionary<int, string> ProcessData(int [] numbers,Person person);
		void PrintData(Dictionary<int, string> data);
		int[] PrepareData(int floor, int ceil);
		
	}
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pluralsight.AdvCShColls.TourBooker.Logic
{
	public class Country
	{
		public string Name { get; }
		public CountryCode Code { get; }
		public string Region { get; }
		public int Population { get; }

		public Country(string name, string code, string region, int population)
		{
			this.Name = name;
			this.Code = new CountryCode(code);
			this.Region = region;
			this.Population = population;
		}

        public override string ToString() => $"{Name} ({Code})";
    }
}

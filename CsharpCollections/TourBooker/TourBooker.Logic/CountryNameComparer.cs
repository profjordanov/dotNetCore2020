using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pluralsight.AdvCShColls.TourBooker.Logic
{
	public class CountryNameComparer : IComparer<Country>
	{
		public static CountryNameComparer Instance { get; } = new CountryNameComparer();
		private CountryNameComparer() { }
		public int Compare(Country x, Country y)
		{
			return x.Name.CompareTo(y.Name);
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GETSOME
{
	public class ComboBoxPairs
	{
		public string _Key { get; set; }
		public string _Value { get; set; }

		public ComboBoxPairs(string _key, string _value)
		{
			_Key = _key;
			_Value = _value;
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GETSOME
{
	// Denne klasse hjælper med at forbinde en key/value pair til de dropdown menuer der er i main window til afdeling & sælger
	// Det gør at vi kan bruge afdelingens ID i databasen til sql queries senere hen, men stadig vise
	// selve navnet i stedet for ID'et i dropdown menuerne
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

using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace GETSOME
{
	// Denne klasse bruges til at tilføje farve (rød, gul, grøn) til datoen i datagrid
	class DateColorConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			Kunde data = (Kunde)value;
			DateTime date = DateTime.Parse(data.Dato.ToString());
			DateTime today = DateTime.Today;
			double diff = (today - date).TotalDays;
			if (diff > 12) return Brushes.Red;
			else if (diff > 7) return Brushes.Yellow;
			else return Brushes.Green;
		}
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}

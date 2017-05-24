using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GETSOME
{
	/// <summary>
	/// Interaction logic for KontaktNoteWindow.xaml
	/// </summary>
	public partial class KontaktNoteWindow : Window
	{
		Kunde kunde;
		public KontaktNoteWindow(Kunde k)
		{
			InitializeComponent();
			kunde = k;

		}
		public void KontaktKunde()
		{
			
		}
	}
}

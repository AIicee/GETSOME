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
		DatabaseAccess da;

		public KontaktNoteWindow(Kunde k, DatabaseAccess d)
		{
			InitializeComponent();
			kunde = k;
			da = d;
		}

		private void KontaktetOK_Click(object sender, RoutedEventArgs e)
		{
			if(da.SetAsContacted(kunde, KontaktetNoteBox.Text))
			{
				this.Close();
			}
		}

		private void KontaktetAnnuller_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}
	}
}

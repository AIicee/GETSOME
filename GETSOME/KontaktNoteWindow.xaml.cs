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
			KundeNavnText.Text = kunde.Navn;
		}

		// Når brugeren klikker på OK knappen indsendes kundens nye data til databasen
		private void KontaktetOK_Click(object sender, RoutedEventArgs e)
		{
			if(da.SetAsContacted(kunde, KontaktetNoteBox.Text))
			{
				this.Close();
			}
		}
		// Når brugeren klikker på annuller knappen, lukkes vinduet
		private void KontaktetAnnuller_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}
		// Når brugeren klikker på enter knappen indsendes kundens nye data til databasen
		private void KontaktNoteBox_EnterKey(object sender, KeyEventArgs e)
		{
			if(e.Key == Key.Enter)
			{
				if (da.SetAsContacted(kunde, KontaktetNoteBox.Text))
				{
					this.Close();
				}
			}
		}
	}
}

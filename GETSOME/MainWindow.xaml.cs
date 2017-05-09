using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GETSOME
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
		DatabaseAccess da;
        public MainWindow()
        {
			da = new DatabaseAccess();
            InitializeComponent();
			// Afdeling combobox
			
			comboBoxAfdeling.ItemsSource = da.GetAfdelinger();
			comboBoxAfdeling.SelectedIndex = 0;
		}

		private void comboBoxAfdeling_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			comboBoxSaelger.ItemsSource = da.GetSaelgere(comboBoxAfdeling.SelectedValue.ToString());
			comboBoxSaelger.SelectedIndex = 0;
		}

		private void comboBoxSaelger_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			da.UpdateDataGrid(dataGridAll);
		}
	}
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
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

			comboBoxAfdeling.DisplayMemberPath = "_Key";
			comboBoxAfdeling.SelectedValuePath = "_Value";
			comboBoxAfdeling.ItemsSource = da.GetAfdelinger();
			comboBoxAfdeling.SelectedIndex = 0;
		}

		private void comboBoxAfdeling_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			comboBoxSaelger.DisplayMemberPath = "_Key";
			comboBoxSaelger.SelectedValuePath = "_Value";
			ComboBoxPairs cbp = (ComboBoxPairs)comboBoxAfdeling.SelectedItem;
			comboBoxSaelger.ItemsSource = da.GetSaelgere(cbp._Value);
			comboBoxSaelger.SelectedIndex = 0;
			ComboBoxPairs cbp2 = (ComboBoxPairs)comboBoxSaelger.SelectedItem;
		}

		private void comboBoxSaelger_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
		}

		private void ButtonUpdateDataGrid_Click(object sender, RoutedEventArgs e)
		{
			
			da.UpdateDataGrid(dataGridAll, comboBoxAfdeling, comboBoxSaelger, DataGridTab.All, TabItemAll);
			da.UpdateDataGrid(dataGridDone, comboBoxAfdeling, comboBoxSaelger, DataGridTab.Done, TabItemDone);
			da.UpdateDataGrid(dataGridAllAndDone, comboBoxAfdeling, comboBoxSaelger, DataGridTab.AllAndDone, TabItemAllDone);
			da.UpdateDataGrid(dataGridRed, comboBoxAfdeling, comboBoxSaelger, DataGridTab.Red, TabItemRed);
			da.UpdateDataGrid(dataGridYellow, comboBoxAfdeling, comboBoxSaelger, DataGridTab.Yellow, TabItemYellow);
			da.UpdateDataGrid(dataGridGreen, comboBoxAfdeling, comboBoxSaelger, DataGridTab.Green, TabItemGreen);
			da.UpdateDataGrid(dataGridExpired, comboBoxAfdeling, comboBoxSaelger, DataGridTab.Expired, TabItemExpired);
            
			
		}
       
        public void doubleclickFunktion(object sender, MouseButtonEventArgs e)
        {
            TabControl TC = tabControl;
            TabItem TI = (TabItem)TC.SelectedItem;
           /* foreach (var control in TI.Content)
            {

            }*/
            Kunde kunde =(Kunde)dataGridAll.SelectedItem ;
            if (e.ClickCount == 2)
            {
                MessageBoxResult result = MessageBox.Show("Vil du godkende kunden?", "Godkendelse", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    da.SetAsContacted(kunde);
                }
            }
            
        }
	}
}

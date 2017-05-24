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
            if (e.ClickCount == 2)
            {
                MessageBoxResult result = MessageBox.Show("Vil du godkende kunden?", "Godkendelse", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
					TabControl TC = tabControl;
					TabItem TI = (TabItem)TC.SelectedItem;
					DataGrid dg;
					switch (TI.Name)
					{
						case "TabItemAll":
							dg = dataGridAll;
							break;
						case "TabItemDone":
							dg = dataGridDone;
							break;
						case "TabItemAllDone":
							dg = dataGridAllAndDone;
							break;
						case "TabItemRed":
							dg = dataGridRed;
							break;
						case "TabItemYellow":
							dg = dataGridYellow;
							break;
						case "TabItemGreen":
							dg = dataGridGreen;
							break;
						case "TabItemExpired":
							dg = dataGridExpired;
							break;
						default:
							dg = null;
							break;
					}
					if(dg == null)
					{
						throw new Exception("FEJL: Der er ikke valgt en kunde.");
					}
					if(da.SetAsContacted((Kunde)dg.SelectedItem))
					{
						MessageBox.Show("Success!");
					}

                }
            }
            
        }
	}
}

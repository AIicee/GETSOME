using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using System.Collections;
using System.Windows.Media;
using System.Text.RegularExpressions;

namespace GETSOME
{
	public enum DataGridTab
	{
		All,
		Done,
		AllAndDone,
		Red,
		Yellow,
		Green,
		Expired
	}
	public class DatabaseAccess
	{

		// Når en kunde er blevet opringet så skal databasen opdateres
		public bool SetAsContacted(Kunde kunde, string note = "")
		{
			// Tjek om kunden allerede er blevet kontaktet
			if (kunde.Kontaktet)
			{
				MessageBox.Show("Denne kunde er allerede blevet kontaktet.");
				return false;
			}
			/*
			// Tjek om kundens data er blevet udfyldt korrekt
			if (!kunde.IsValid())
			{
				throw new Exception("FEJL: SetAsContacted(kunde) -> kunde.IsValid() -> Kunden er ikke korrekt valideret");
			}
			*/
			SqlConnection sql = new SqlConnection("Data Source=ealdb1.eal.local;Initial Catalog=EJL34_DB;User ID=ejl34_usr;Password=Baz1nga34");
			try
			{
				sql.Open();
			}
			catch (SqlException)
			{
				MessageBox.Show("FEJL: SetAsContacted() kunne ikke få adgang til databasen.");
				return false;
			}
			// Når alt er OK sendes en opdatering til databasen som sætter kunden som kontaktet + en mulig note
			string query = "Update Karvil_Kunder SET Kontaktet = 1 "+(note != "" ? ", Note = @note":"")+" WHERE ID = " + kunde.ID;
			SqlCommand cmd = new SqlCommand(query, sql);
			cmd.Parameters.AddWithValue("@note", note);
			cmd.ExecuteNonQuery();
			sql.Close();
			// Kundens data blev sendt til databasen og er blevet opdateret
			return true;
		}

		// Henter alle afdelinger fra databasen til udfyldelse af dropdown menuen i main window
		public List<ComboBoxPairs> GetAfdelinger()
		{
			
			List<ComboBoxPairs> list = new List<ComboBoxPairs>();
			list.Add(new ComboBoxPairs("All", "0"));
			SqlConnection sql = new SqlConnection("Data Source=ealdb1.eal.local;Initial Catalog=EJL34_DB;User ID=ejl34_usr;Password=Baz1nga34");
			try
			{
				sql.Open();
			}
			catch (SqlException)
			{
				MessageBox.Show("FEJL: GetAfdelinger() kunne ikke få adgang til databasen.");
				return list;
			}
			string query = "SELECT ID, Navn FROM Karvil_Afdeling ORDER BY Navn ASC";
			SqlCommand cmd = new SqlCommand(query, sql);
			SqlDataReader reader = cmd.ExecuteReader();
			if (reader.HasRows)
			{
				while (reader.Read())
				{
					list.Add(new ComboBoxPairs(reader["Navn"].ToString(), reader["ID"].ToString()));
				}
			}
			sql.Close();
			return list;
		}

		// Henter alle sælgere fra databasen til udfyldelse af dropdown menuen i main window
		public List<ComboBoxPairs> GetSaelgere(string afdeling)
		{
			List<ComboBoxPairs> list = new List<ComboBoxPairs>();
			list.Add(new ComboBoxPairs("All", "0"));
			SqlConnection sql = new SqlConnection("Data Source=ealdb1.eal.local;Initial Catalog=EJL34_DB;User ID=ejl34_usr;Password=Baz1nga34");
			try
			{
				sql.Open();
			}
			catch (SqlException)
			{
				MessageBox.Show("FEJL: GetSaelgere() kunne ikke få adgang til databasen.");
				return list;
			}
			
			string query = "SELECT ID, Navn FROM Karvil_Saelger";
			if(afdeling != "0")
			{
				query += " WHERE AfdelingID = "+afdeling;
			}
			query += " ORDER BY Navn ASC";

			SqlCommand cmd = new SqlCommand(query, sql);
			SqlDataReader reader = cmd.ExecuteReader();
			if (reader.HasRows)
			{
				while (reader.Read())
				{
					list.Add(new ComboBoxPairs(reader["Navn"].ToString(), reader["ID"].ToString()));
				}
			}
			sql.Close();
			return list;
		}

		// et datagrid bliver fyldt ud med kunder fra databasen.
		// dataen bliver uddelt mellem 7 forskellige grids alt efter kundens data (kontaktet & dato)
		public void UpdateDataGrid(DataGrid dg, ComboBox cbAfdeling, ComboBox cbSaelger, DataGridTab tab, TabItem header)
		{
			// Slet gammel data først
			dg.Items.Clear();
			dg.Items.Refresh();

			SqlConnection sql = new SqlConnection("Data Source=ealdb1.eal.local;Initial Catalog=EJL34_DB;User ID=ejl34_usr;Password=Baz1nga34");
			try
			{
				sql.Open();
			}
			catch (SqlException)
			{
				MessageBox.Show("FEJL: UpdateDataGrid() kunne ikke få adgang til databasen.");
			}

			// En query bliver bygget alt efter den valgte datagrid tab
			string query = "";
			switch (tab)
			{
				case DataGridTab.All:
					query = "SELECT Karvil_Kunder.ID, Karvil_Kunder.Navn, Karvil_Kunder.Tlf, Karvil_Kunder.Type, Karvil_Kunder.Dato, Karvil_Kunder.Kontaktet, Karvil_Kunder.Note, Karvil_Kunder.SaelgerID, Karvil_Saelger.Navn as SaelgerNavn FROM Karvil_Kunder INNER JOIN Karvil_Saelger ON Karvil_Kunder.SaelgerID = Karvil_Saelger.ID WHERE Karvil_Kunder.Kontaktet = 0 AND DATEDIFF(DAY, Karvil_Kunder.Dato, getdate()) <= 14 ";
					break;
				case DataGridTab.Done:
					query = "SELECT Karvil_Kunder.ID, Karvil_Kunder.Navn, Karvil_Kunder.Tlf, Karvil_Kunder.Type, Karvil_Kunder.Dato, Karvil_Kunder.Kontaktet, Karvil_Kunder.Note, Karvil_Kunder.SaelgerID, Karvil_Saelger.Navn as SaelgerNavn FROM Karvil_Kunder INNER JOIN Karvil_Saelger ON Karvil_Kunder.SaelgerID = Karvil_Saelger.ID WHERE Karvil_Kunder.Kontaktet = 1 ";
					break;
				case DataGridTab.AllAndDone:
					query = "SELECT Karvil_Kunder.ID, Karvil_Kunder.Navn, Karvil_Kunder.Tlf, Karvil_Kunder.Type, Karvil_Kunder.Dato, Karvil_Kunder.Kontaktet, Karvil_Kunder.Note, Karvil_Kunder.SaelgerID, Karvil_Saelger.Navn as SaelgerNavn FROM Karvil_Kunder INNER JOIN Karvil_Saelger ON Karvil_Kunder.SaelgerID = Karvil_Saelger.ID WHERE (Karvil_Kunder.Kontaktet = 1 OR (Karvil_Kunder.Kontaktet = 0 AND DATEDIFF(DAY, Karvil_Kunder.Dato, getdate()) <= 14)) ";
					break;
				case DataGridTab.Red:
					query = "SELECT Karvil_Kunder.ID, Karvil_Kunder.Navn, Karvil_Kunder.Tlf, Karvil_Kunder.Type, Karvil_Kunder.Dato, Karvil_Kunder.Kontaktet, Karvil_Kunder.Note, Karvil_Kunder.SaelgerID, Karvil_Saelger.Navn as SaelgerNavn FROM Karvil_Kunder INNER JOIN Karvil_Saelger ON Karvil_Kunder.SaelgerID = Karvil_Saelger.ID WHERE Karvil_Kunder.Kontaktet = 0 AND DATEDIFF(DAY, Karvil_Kunder.Dato, getdate()) > 12 ";
					break;
				case DataGridTab.Yellow:
					query = "SELECT Karvil_Kunder.ID, Karvil_Kunder.Navn, Karvil_Kunder.Tlf, Karvil_Kunder.Type, Karvil_Kunder.Dato, Karvil_Kunder.Kontaktet, Karvil_Kunder.Note, Karvil_Kunder.SaelgerID, Karvil_Saelger.Navn as SaelgerNavn FROM Karvil_Kunder INNER JOIN Karvil_Saelger ON Karvil_Kunder.SaelgerID = Karvil_Saelger.ID WHERE Karvil_Kunder.Kontaktet = 0 AND DATEDIFF(DAY, Karvil_Kunder.Dato, getdate()) > 7 AND DATEDIFF(DAY, Karvil_Kunder.Dato, getdate()) <= 12 ";
					break;
				case DataGridTab.Green:
					query = "SELECT Karvil_Kunder.ID, Karvil_Kunder.Navn, Karvil_Kunder.Tlf, Karvil_Kunder.Type, Karvil_Kunder.Dato, Karvil_Kunder.Kontaktet, Karvil_Kunder.Note, Karvil_Kunder.SaelgerID, Karvil_Saelger.Navn as SaelgerNavn FROM Karvil_Kunder INNER JOIN Karvil_Saelger ON Karvil_Kunder.SaelgerID = Karvil_Saelger.ID WHERE Karvil_Kunder.Kontaktet = 0 AND DATEDIFF(DAY, Karvil_Kunder.Dato, getdate()) <= 7 ";
					break;
				case DataGridTab.Expired:
					query = "SELECT Karvil_Kunder.ID, Karvil_Kunder.Navn, Karvil_Kunder.Tlf, Karvil_Kunder.Type, Karvil_Kunder.Dato, Karvil_Kunder.Kontaktet, Karvil_Kunder.Note, Karvil_Kunder.SaelgerID, Karvil_Saelger.Navn as SaelgerNavn FROM Karvil_Kunder INNER JOIN Karvil_Saelger ON Karvil_Kunder.SaelgerID = Karvil_Saelger.ID WHERE Karvil_Kunder.Kontaktet = 0 AND DATEDIFF(DAY, Karvil_Kunder.Dato, getdate()) > 14 ";
					break;

			}

			ComboBoxPairs cbpAfdeling = (ComboBoxPairs)cbAfdeling.SelectedItem;
			ComboBoxPairs cbpSaelger = (ComboBoxPairs)cbSaelger.SelectedItem;

			// Hvis der er specificeret en afdeling
			if (cbpAfdeling._Value != "0")
			{
				query += " AND Karvil_Saelger.AfdelingID = "+cbpAfdeling._Value;

				// og hvis der er specificeret en sælger
				if(cbpSaelger._Value != "0")
				{
					query += " AND Karvil_Kunder.SaelgerID = "+cbpSaelger._Value;
				}
			
			// Hvis der kun er specificeret en sælger (ingen afdeling)
			}else if (cbpSaelger._Value != "0")
			{
				query += " AND Karvil_Kunder.SaelgerID = " + cbpSaelger._Value;
			}
			query += " ORDER BY Karvil_Kunder.Dato ASC";
			SqlCommand cmd = new SqlCommand(query, sql);
			SqlDataReader reader = cmd.ExecuteReader();
			if (reader.HasRows)
			{
				int rows = 0;
				while (reader.Read())
				{
					// Dataen fra databasen bliver læst og indsat i kunde objekter
					rows++;
					dg.Items.Add(new Kunde()
					{
						ID = Convert.ToInt32(reader["ID"]),
						Navn = reader["Navn"].ToString(),
						Tlf = reader["Tlf"].ToString(),
						Type = reader["Type"].ToString(),
						Dato = Convert.ToDateTime(reader["Dato"]),
						Kontaktet = (Convert.ToInt32(reader["Kontaktet"]) == 1 ? true : false),
						Note = reader["Note"].ToString(),
						SaelgerID = Convert.ToInt32(reader["SaelgerID"]),
						SaelgerNavn = reader["SaelgerNavn"].ToString()
					});
				}

				// Datagrid tab-navnene bliver opdateret med en sum af alle kunder i den tab
				Regex re = new Regex("[a-z+]+", RegexOptions.IgnoreCase);
				Match m = re.Match(header.Header.ToString());
				header.Header = m.Value + " ("+rows+")";
			}
			sql.Close();
		}
	}
	

}

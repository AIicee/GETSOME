﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace GETSOME
{

		public class DatabaseAccess
		{

			bool SetAsContacted(Kunde kunde)
			{
				SqlConnection sql = new SqlConnection("Data Source=ealdb1.eal.local;Initial Catalog=EJL34_DB;User ID=ejl34_usr;Password=Baz1nga34");
				try
				{
					sql.Open();
				}
				catch (Exception)
				{
					MessageBox.Show("Der skete en fejl.");
					return false;
				}
				string query = "Update Karvil_Kunder SET Kontaktet = 1 WHERE ID = " + kunde.ID;
				SqlCommand cmd = new SqlCommand(query, sql);
				cmd.ExecuteNonQuery();
				sql.Close();
				return true;
			}

			public void UpdateDataGrid(DataGrid dg)
			{
				dg.Items.Clear();
				dg.Items.Refresh();

				SqlConnection sql = new SqlConnection("Data Source=ealdb1.eal.local;Initial Catalog=EJL34_DB;User ID=ejl34_usr;Password=Baz1nga34");
				try
				{
					sql.Open();
					sql.Close();
				}
				catch (Exception)
				{
					MessageBox.Show("Der skete en fejl.");
				}
				sql.Open();
				string query = "SELECT Karvil_Kunder.ID, Karvil_Kunder.Navn, Karvil_Kunder.Tlf, Karvil_Kunder.Type, Karvil_Kunder.Dato, Karvil_Kunder.Kontaktet, Karvil_Kunder.Note, Karvil_Kunder.SaelgerID, Karvil_Saelger.Navn as SaelgerNavn FROM Karvil_Kunder INNER JOIN Karvil_Saelger ON Karvil_Kunder.SaelgerID = Karvil_Saelger.ID";
				SqlCommand cmd = new SqlCommand(query, sql);
				SqlDataReader reader = cmd.ExecuteReader();
				if (reader.HasRows)
				{
					int rows = 0;
					while (reader.Read())
					{
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
				}
			}
		}
	

}

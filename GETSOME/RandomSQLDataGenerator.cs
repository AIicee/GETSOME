using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GETSOME
{
	public class RandomSQLDataGenerator
	{
		public void Generate()
		{
			string[] names = File.ReadAllLines("../../randomnames.txt");
			string[] typer = new string[] {"Ny Bil", "Reparation", "Andet"};
			int[] kontaktet = new int[] {0, 1};
			Random rand = new Random();
			SQLSimplifier sql = new SQLSimplifier();
			sql.Connect("ealdb1.eal.local", "EJL34_DB", "ejl34_usr", "Baz1nga34");

			for(int i = 0; i < 20; i++)
			{
				Dictionary<string, string> data = new Dictionary<string, string>();
				data.Add("Navn", names[rand.Next(names.Length)]);
				data.Add("AfdelingID", (rand.Next(5) + 1).ToString());
				int insertedID = sql.Insert("Karvil_Saelger", data);
				for(int u = 0; u < 30; u++)
				{
					Dictionary<string, string> data2 = new Dictionary<string, string>();
					data2.Add("Navn", names[rand.Next(names.Length)]);
					data2.Add("Tlf", rand.Next(11111111,99999999).ToString());
					data2.Add("Type", typer[rand.Next(typer.Length)]);
					DateTime start = new DateTime(2017, 5, 1);
					int range = (DateTime.Today - start).Days + 50;
					start = start.AddDays(rand.Next(range));
					data2.Add("Dato", start.ToString("yyyy-MM-dd"));
					data2.Add("Kontaktet", rand.Next(kontaktet.Length).ToString());
					data2.Add("SaelgerID", insertedID.ToString());
					sql.Insert("Karvil_Kunder", data2);
				}
			}

		}
	}
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GETSOME
{
	// En klasse designet til at generere random test kunder til databasen
	public class RandomSQLDataGenerator
	{
		public void Generate()
		{
			// en liste med 500 tilfældigt genereret navnet bliver indlæst
			string[] names = File.ReadAllLines("../../randomnames.txt");
			// 3 forskellige typer af salg
			string[] typer = new string[] {"Ny Bil", "Reparation", "Andet"};
			// en kunde bliver enten sat til at have været kontaktet eller ikke
			int[] kontaktet = new int[] {0, 1};
			Random rand = new Random();
			// Forbind til databasen
			SQLSimplifier sql = new SQLSimplifier();
			sql.Connect("ealdb1.eal.local", "EJL34_DB", "ejl34_usr", "Baz1nga34");
			// Opretter 20 forskellige sælgere
			for(int i = 0; i < 20; i++)
			{
				Dictionary<string, string> data = new Dictionary<string, string>();
				// indsæt et tilfældigt navn i den nuværende iteration
				data.Add("Navn", names[rand.Next(names.Length)]);
				// indsæt et tilfældigt afdeling's ID i den nuværende iteration
				data.Add("AfdelingID", (rand.Next(5) + 1).ToString());
				// Indsætter sælgeren i databasen og gemmer det unikt tildelte ID den får genereret automatisk af databasen
				int insertedID = sql.Insert("Karvil_Saelger", data);
				// Opretter nu 30 forskellige kunder tildelt den ovenstående sælger
				for(int u = 0; u < 30; u++)
				{
					Dictionary<string, string> data2 = new Dictionary<string, string>();
					// Et tilfældigt navn, tlf, type og dato bliver genereret
					data2.Add("Navn", names[rand.Next(names.Length)]);
					data2.Add("Tlf", rand.Next(11111111,99999999).ToString());
					data2.Add("Type", typer[rand.Next(typer.Length)]);
					DateTime start = new DateTime(2017, 5, 1);
					int range = (DateTime.Today - start).Days + 50;
					start = start.AddDays(rand.Next(range));
					data2.Add("Dato", start.ToString("yyyy-MM-dd"));
					data2.Add("Kontaktet", rand.Next(kontaktet.Length).ToString());
					data2.Add("SaelgerID", insertedID.ToString());
					// kunden bliver indsat i databasen
					sql.Insert("Karvil_Kunder", data2);
				}
			}

		}
	}
}

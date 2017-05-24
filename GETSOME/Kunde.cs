using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace GETSOME
{
	// Kunde klassen
	public class Kunde
	{
		public int ID { get; set; }
		public string Navn { get; set; }
		public string Tlf { get; set; }
		public string Type { get; set; }
		public DateTime Dato { get; set; }
		public bool Kontaktet { get; set; }

		// Bruges til at konvertere en boolean til en string, så den kan vises som tekst i datagrid
		public string KontaktetString
		{
			get { return Kontaktet == true ? "Ja" : "Nej"; }
		}
		public string Note { get; set; }
		public int SaelgerID { get; set; }
		public string SaelgerNavn { get; set; }

		// Tjekker om kundens data er blevet hentet/indtastet korrekt
		public bool IsValid()
		{
			return ID != 0;
		}
	}
	
}

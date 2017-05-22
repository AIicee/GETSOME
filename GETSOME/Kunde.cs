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
	public class Kunde
	{
		public int ID { get; set; }
		public string Navn { get; set; }
		public string Tlf { get; set; }
		public string Type { get; set; }
		public DateTime Dato { get; set; }
		public bool Kontaktet { get; set; }
		public string KontaktetString
		{
			get { return Kontaktet == true ? "Ja" : "Nej"; }
		}
		public string Note { get; set; }
		public int SaelgerID { get; set; }
		public string SaelgerNavn { get; set; }

		public bool IsValid()
		{
			if(ID == 0)
			{
				return false;
			}
			return true;
		}
	}
	
}

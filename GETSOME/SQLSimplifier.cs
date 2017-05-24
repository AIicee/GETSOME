using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace GETSOME
{
	public class SQLSimplifier
	{
		SqlConnection sql;

		public bool Connect(string server, string db_name, string user_id, string user_password)
		{
			sql = new SqlConnection("Data Source=" + server + ";Initial Catalog=" + db_name + ";User ID=" + user_id + ";Password=" + user_password);
			try
			{
				sql.Open();
				sql.Close();
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}
		public bool Connect(string connectionString)
		{
			sql = new SqlConnection(connectionString);
			try
			{
				sql.Open();
				sql.Close();
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}

		public SqlConnection GetConnection()
		{
			return sql;
		}

		public void Dispose()
		{
			sql.Dispose();
		}

		public int Insert(string table, Dictionary<string, string> data)
		{
			string query = "INSERT INTO " + table + " ";
			string names = "";
			string values = "";
			foreach (KeyValuePair<string, string> row in data)
			{
				names += row.Key + ",";
				values += "@" + row.Key + ",";
			}
			names = names.TrimEnd(',');
			values = values.TrimEnd(',');
			query += "(" + names + ") OUTPUT INSERTED.ID VALUES(" + values + ")";
			try
			{
				sql.Open();
				SqlCommand cmd = new SqlCommand(query, sql);
				foreach (KeyValuePair<string, string> row in data)
				{
					cmd.Parameters.AddWithValue(row.Key, row.Value);
				}
				int id = (int)cmd.ExecuteScalar();
				sql.Close();
				return id;
			}
			catch (Exception)
			{
				return 0;
			}
		}

		public Dictionary<string, string> Select(string table, string data, string conditions = null)
		{ // Ikke færdig
			string query = "SELECT " + data + " FROM " + table;
			if (conditions != null)
			{
				query += " " + conditions;
			}
			SqlCommand cmd = new SqlCommand(query, sql);
			sql.Open();
			SqlDataReader reader = cmd.ExecuteReader();
			if (reader.HasRows)
			{
				while (reader.Read())
				{
					try
					{
						Console.WriteLine("Fornavn: " + reader.GetString(0) + "; Efternavn: " + reader.GetString(1));
					}
					catch (Exception e)
					{
						Console.WriteLine(e.ToString());
					}
				}
			}
			sql.Close();
			return null;
		}

		public int Update(string table, Dictionary<string, string> data, int id)
		{

			string query = "UPDATE " + table + " SET ";
			foreach (KeyValuePair<string, string> row in data)
			{
				query += row.Key + "=@" + row.Key + ",";
			}
			query = query.TrimEnd(',');
			query += " WHERE ID=@ID";
			SqlCommand cmd = new SqlCommand(query, sql);
			foreach (KeyValuePair<string, string> row in data)
			{
				cmd.Parameters.AddWithValue(row.Key, row.Value);
			}
			cmd.Parameters.AddWithValue("ID", id);
			sql.Open();
			int i = cmd.ExecuteNonQuery();
			sql.Close();
			return i;
		}

		public int Delete(string table, string row, string value)
		{
			SqlCommand cmd = new SqlCommand("DELETE FROM " + table + " WHERE " + row + "=@" + row, sql);
			cmd.Parameters.AddWithValue(row, value);
			sql.Open();
			int i = cmd.ExecuteNonQuery();
			sql.Close();
			return i;
		}

		public void Search()
		{
			string query = "SELECT Fornavn, Tlf FROM AarskortMomentum WHERE Fornavn LIKE '%mat%'";
			SqlCommand cmd = new SqlCommand(query, sql);
			sql.Open();
			SqlDataReader reader = cmd.ExecuteReader();
			if (reader.HasRows)
			{
				while (reader.Read())
				{
					Console.WriteLine("Fornavn: " + reader["Fornavn"] + "; Tlf: " + reader["Tlf"]);
				}
			}
			sql.Close();
		}
	}
}
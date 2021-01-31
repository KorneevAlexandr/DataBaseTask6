using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace LibraryToSQL
{
	/// <summary>
	/// Implementation of the principle CRUD(create, read, update, delete)
	/// Class-singleton
	/// </summary>
	public sealed class CRUD
	{
		/// <summary>
		/// Database connection object
		/// </summary>
		private SqlConnection sqlConnection = null;

		/// <summary>
		/// Static constructor
		/// </summary>
		static CRUD() { }
		/// <summary>
		/// Initializing a database connection object
		/// </summary>
		private CRUD()
		{
			sqlConnection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Учеба\Training\DataBaseTask6\LibraryToSQL\DataBase.mdf;Integrated Security=True");
		}

		/// <summary>
		/// Properties for create one instance class
		/// </summary>
		public static CRUD Sourse { get => new CRUD(); }

		/// <summary>
		/// Properties for testing connection
		/// </summary>
		public bool Connection
		{
			get
			{
				sqlConnection.Open();
				if (sqlConnection.State == ConnectionState.Open)
				{
					sqlConnection.Close();
					return true;
				}
				else
				{
					sqlConnection.Close();
					return false;
				}
			}
		}

		/// <summary>
		/// Create new row in database
		/// </summary>
		/// <param name="student">Object student for new row</param>
		public void Create(Student student)
		{
			sqlConnection.Open();

			string select = String.Format("INSERT INTO Students " +
				"VALUES (N'{0}', N'{1}', N'{2}', N'{3}', '{4}', N'{5}', " +
				"N'{6}', '{7}', N'{8}', N'{9}', '{10}', N'{11}', N'{12}', '{13}', N'{14}')",
				student.Surname, student.Name, student.Patronymic, student.Sex, student.DateBirt(), student.NameGroup,
				student.ExName(0), student.ExDate(0), student.ExMark(0),
				student.ExName(1), student.ExDate(1), student.ExMark(1),
				student.ExName(2), student.ExDate(2), student.ExMark(2));

			SqlCommand command = new SqlCommand(select, sqlConnection);
			command.ExecuteNonQuery();

			sqlConnection.Close();

		}

		/// <summary>
		/// Read table in database
		/// </summary>
		/// <param name="selection">Query to database</param>
		/// <param name="x">Numbers columns</param>
		/// <returns>Collection string</returns>
		public List<string> Read(string selection, int x)
		{
			sqlConnection.Open();
			List<string> Stud = new List<string>();

			SqlCommand command = new SqlCommand(selection, sqlConnection);
			SqlDataReader reader = command.ExecuteReader();

			if (reader.HasRows) // There is data
			{
				Console.WriteLine();
				while (reader.Read()) // read data row
				{
					Stud.Add("");
					for (int i = 0; i < x; i++)
					{
						Stud[Stud.Count - 1] += String.Concat(reader.GetValue(i).ToString(), " ");
					}
				}
			}

			sqlConnection.Close();
			return Stud;
		}

		/// <summary>
		/// Delete student by name and surname
		/// </summary>
		/// <param name="name">Name student</param>
		/// <param name="surname">Surname student</param>
		public void Delete(string name, string surname)
		{
			sqlConnection.Open();

			SqlCommand command = new SqlCommand("DELETE FROM [Students]" +
				" WHERE SurName=N'" + surname + "' AND StudentName = N'" + name + "'", sqlConnection);

			command.ExecuteNonQuery();  // int x = ... (кол-во удаленных строк)	

			sqlConnection.Close();
		}

		/// <summary>
		/// Change data by examen mark in database
		/// </summary>
		/// <param name="number">Number examen</param>
		/// <param name="name">Name student</param>
		/// <param name="surname">Surname student</param>
		/// <param name="mark">New mark</param>
		public void UpdateEx(int number, string name, string surname, int mark)
		{
			sqlConnection.Open();
			string up;
			up = "UPDATE [Students] " +
				"SET Mark_" + number.ToString() + " = " + mark + " WHERE " +
				"StudentName = N'" + name + "' AND SurName = N'" + surname + "'";

			SqlCommand command = new SqlCommand(up, sqlConnection);
			command.ExecuteNonQuery();

			sqlConnection.Close();
		}

		/// <summary>
		/// Override method ToString()
		/// </summary>
		/// <returns>Strok about connection</returns>
		public override string ToString()
		{
			if (sqlConnection != null)
				return "Connection installed";
			return "Connection not installed";
		}

		/// <summary>
		/// Override method Equals
		/// </summary>
		/// <param name="obj">Object</param>
		/// <returns>True or false</returns>
		public override bool Equals(object obj)
		{
			if (obj == null)
				return false;
			CRUD crud = (CRUD)obj;
			if (crud.sqlConnection == sqlConnection)
				return true;
			return false;
		}

		/// <summary>
		/// Hash-func
		/// </summary>
		/// <returns>1 or 0 (connection / not connection)</returns>
		public override int GetHashCode()
		{
			if (sqlConnection != null)
				return 1;
			return 0;
		}

	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibraryToSQL
{
	/// <summary>
	/// Class for creating a list of students with different parameters
	/// </summary>
	public class CreateList
	{
		/// <summary>
		/// An instance of the CRUD implementation for the database
		/// </summary>
		private CRUD cRUD;
		/// <summary>
		/// Query to get a list of all groups in the database
		/// </summary>
		private string selection_group = "SELECT DISTINCT StudentGroup FROM [Students]";
		/// <summary>
		/// List of all groups in a single instance
		/// </summary>
		private List<string> Groups;

		/// <summary>
		/// Initializing CRUD Object and Group List
		/// </summary>
		public CreateList()
		{
			cRUD = CRUD.Sourse;
			Groups = cRUD.Read(selection_group, 1);
		}

		/// <summary>
		/// Retrieving information from the entire table
		/// </summary>
		/// <returns>An array of strings containing each row from a database table</returns>
		public string[] GetAllStudents()
		{
			string selection = "SELECT * FROM [Students]";
			List<string> Students = cRUD.Read(selection, 15);
			return Students.ToArray();
		}

		/// <summary>
		/// Getting students for expulsion
		/// Students are sorted by group
		/// </summary>
		/// <returns>Array of strings with information about students for expulsion</returns>
		public string[] GetOutStudents()
		{
			string selection = "SELECT SurName, StudentName, StudentGroup, Mark_1, Mark_2, Mark_3 " +
				"FROM [Students] WHERE Mark_1 < 4 AND Mark_2 < 4 AND Mark_3 < 4";
			List<string> Students = cRUD.Read(selection, 6);

			IEnumerable<string> StudentsOut = from g in Groups
						from t in Students
								where t.Split(' ')[2].Trim() == g.Trim()
									select t;

			return StudentsOut.ToArray();
		}

		/// <summary>
		/// Processing string for find max, min, sum marks amd numerous students specifics group
		/// </summary>
		/// <param name="s">String with marks</param>
		/// <param name="min">Min mark</param>
		/// <param name="max">Max mark</param>
		/// <param name="sum">Sum marks</param>
		/// <param name="numerous">Numerous srecific students</param>
		private void Processing(string s, ref double min, ref double max, ref double sum, ref int numerous)
		{
			double midd = (Convert.ToDouble(s.Split(' ')[1]) +
							Convert.ToDouble(s.Split(' ')[2]) +
							Convert.ToDouble(s.Split(' ')[3])) / 3;

			sum += midd;
			numerous++;

			if (max < midd) max = midd;
			if (min > midd) min = midd;
		}

		/// <summary>
		/// Method of obtaining information about average / minimum / maximum scores for a session.
		/// Students are sorted by group
		/// </summary>
		/// <returns>An array of rows with scores for each group</returns>
		public string[] GetResultMark()
		{
			string selection = "SELECT StudentGroup, Mark_1, Mark_2, Mark_3 FROM [Students]";
			List<string> Students = cRUD.Read(selection, 4);

			List<string> Result = new List<string>();

			// I couldn't use linq, complex condition :(

			int numerous;
			double sum, min, max;
			foreach (string G in Groups)
			{
				min = 10;
				max = 0;
				sum = 0;
				numerous = 0;
				for (int i = 0; i < Students.Count; i++)
					if (Students[i].Split(' ')[0].Trim() == G.Trim())
						Processing(Students[i], ref min, ref max, ref sum, ref numerous);

				Result.Add( String.Concat(
					G.Trim(), " ", Math.Round((sum / numerous), 1).ToString(), " ",
					Math.Round(min, 1).ToString() + " " + Math.Round(max, 1).ToString() ));
			}

			return Result.ToArray();
		}

		/// <summary>
		/// Receiving a list of students with information about passing the session
		/// Students are sorted by group
		/// </summary>
		/// <returns>An array with strings containing information about the delivery of the session by students</returns>
		public string[] GetResultSession()
		{
			string selection = "SELECT SurName, StudentName, StudentGroup, " +
				"ExamenName_1, Mark_1, ExamenName_2, Mark_2, ExamenName_3, Mark_3 FROM [Students]";
			List<string> Students = cRUD.Read(selection, 9);

			StringBuilder[] sb = new StringBuilder[Groups.Count];

			IEnumerable<string> Result = from g in Groups
									 	 from s in Students
										     where s.Split(' ')[2].Trim() == g.Trim()
											 select s;

			return Result.ToArray();
		}

		/// <summary>
		/// Override method ToString
		/// </summary>
		/// <returns>String groups</returns>
		public override string ToString()
		{
			string[] mas = new string[Groups.Count];
			for (int i = 0; i < Groups.Count; i++)
				mas[i] = Groups[i];
			return String.Join(", ", mas);
		}

		/// <summary>
		/// Override method Equals
		/// </summary>
		/// <param name="obj">Object for compare</param>
		/// <returns>true or false</returns>
		public override bool Equals(object obj)
		{
			if (obj == null)
				return false;

			CreateList create = (CreateList)obj;

			if (cRUD != create.cRUD || selection_group != create.selection_group 
				|| Groups.Count != create.Groups.Count)
				return false;
			for (int i = 0; i < Groups.Count; i++)
				if (Groups[i] != create.Groups[i])
					return false;
			return true;

		}

		/// <summary>
		/// Hash-func
		/// </summary>
		/// <returns>Numbers groups</returns>
		public override int GetHashCode()
		{
			return Groups.Count;
		}

	}
}

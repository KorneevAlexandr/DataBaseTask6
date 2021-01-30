using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
			cRUD = new CRUD();
			Groups = cRUD.Read(selection_group, 1);
		}



		/// <summary>
		/// Retrieving information from the entire table
		/// </summary>
		/// <returns><An array of strings containing each row from a database table/returns>
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

			string[] StudentsOut = new string[Students.Count];

			// sort
			int index = 0;
			int j = 0;
			while (index < Groups.Count)
			{
				for (int i = 0; i < Students.Count; i++)
					if (Students[i].Split(' ')[2].Trim() == Groups[index].Trim())
					{
						StudentsOut[j] = Students[i];
						j++;
					}
				index++;
			}

			return StudentsOut;
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

			List<string> Result = Groups;

			int index = 0, numerous;
			double sum, min, max;
			while (index < Groups.Count)
			{
				min = 10;
				max = 0;
				sum = 0;
				numerous = 0;
				for (int i = 0; i < Students.Count; i++)
				{
					if (Students[i].Split(' ')[0].Trim() == Groups[index].Trim())
					{
						double midd = (Convert.ToDouble(Students[i].Split(' ')[1]) +
							Convert.ToDouble(Students[i].Split(' ')[2]) +
							Convert.ToDouble(Students[i].Split(' ')[3])) / 3;

						sum += midd;
						numerous++;

						if (max < midd) max = midd;
						if (min > midd) min = midd;
					}
				}
				Result[index] += " " + Math.Round((sum / numerous), 1).ToString();
				Result[index] += " " + Math.Round(min, 1).ToString() + " " + Math.Round(max, 1).ToString();
				index++;
			}

			return Result.ToArray();
		}

		/// <summary>
		/// Receiving a list of students with information about passing the session
		/// Students are sorted by group
		/// </summary>
		/// <returns>An array with strings containing information about the delivery of the session by students</returns>
		public StringBuilder[] GetResultSession()
		{
			string selection = "SELECT SurName, StudentName, StudentGroup, " +
				"ExamenName_1, Mark_1, ExamenName_2, Mark_2, ExamenName_3, Mark_3 FROM [Students]";
			List<string> Students = cRUD.Read(selection, 9);

			StringBuilder[] sb = new StringBuilder[Groups.Count];

			int index = 0;
			while (index < Groups.Count)
			{
				sb[index] = new StringBuilder();
				for (int i = 0; i < Students.Count; i++)
					if (Students[i].Split(' ')[2].Trim() == Groups[index].Trim())
					{
						sb[index].Append(Students[i]);
						sb[index].AppendLine();
					}
				index++;
			}

			return sb;
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

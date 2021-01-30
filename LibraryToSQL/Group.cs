using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryToSQL
{
	/// <summary>
	/// Description of the group of students
	/// </summary>
	public class Group
	{
		/// <summary>
		/// Name group
		/// </summary>
		public string NameGroup { get; set; }
		/// <summary>
		/// List student current group
		/// </summary>
		private List<Student> Students;

		/// <summary>
		/// Get exams at current group
		/// </summary>
		public string[] GetExamens
		{
			get
			{
				if (Students.Count != 0)
					return new string[3] { Students[0].ExName(0), Students[0].ExName(1), Students[0].ExName(2) };
				else
					throw new Exception("There are no students in the group ");
			}
		}

		/// <summary>
		/// Get date exams at current group
		/// </summary>
		public string[] GetDatesExamens
		{
			get
			{
				if (Students.Count != 0)
					return new string[3] { Students[0].ExDate(0), Students[0].ExDate(1), Students[0].ExDate(2) };
				else
					throw new Exception("There are no students in the group ");
			}
		}
		/// <summary>
		/// Get student by index
		/// </summary>
		/// <param name="index">Index</param>
		/// <returns>Student</returns>
		public Student this[int index] { get => Students[index]; }

		/// <summary>
		/// Get numerous group
		/// </summary>
		public int Numbers { get => Students.Count; }

		/// <summary>
		/// Inicialization group
		/// </summary>
		/// <param name="nameGroup">Name group</param>
		public Group(string nameGroup)
		{
			NameGroup = nameGroup;
			Students = new List<Student>();
		}

		/// <summary>
		/// Add student in group
		/// </summary>
		/// <param name="student">Student</param>
		public void AddStudent(Student student)
		{
			Students.Add(student);
		}

		/// <summary>
		/// Data about group
		/// </summary>
		/// <returns>Strok with name and numbers group</returns>
		public override string ToString()
		{
			return String.Concat(NameGroup, " ", Numbers.ToString(), " students");
		}
		/// <summary>
		/// Compare two objects
		/// </summary>
		/// <param name="obj">Object</param>
		/// <returns>True or false</returns>
		public override bool Equals(object obj)
		{
			if (obj == null)
				return false;
			Group g = (Group)obj;

			if (NameGroup != g.NameGroup || Numbers != g.Numbers)
				return false;
			for (int i = 0; i < Students.Count; i++)
				if (Students[i].Equals(g.Students[i]) == false)
					return false;

			return true;
		}
		/// <summary>
		/// Hash-func
		/// </summary>
		/// <returns>Numbers students</returns>
		public override int GetHashCode()
		{
			return Numbers;
		}

	}
}

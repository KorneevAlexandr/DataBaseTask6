using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryToSQL
{
	/// <summary>
	/// Class for student description
	/// </summary>
	public class Student
	{
		/// <summary>
		/// A nested class for adding exams to the student
		/// </summary>
		public class Examen
		{
			/// <summary>
			/// Examen name
			/// </summary>
			public string ExamenName { get; set; }
			/// <summary>
			/// Student mark for the exam
			/// </summary>
			public int Mark { get; set; }
			/// <summary>
			/// Date examen
			/// </summary>
			public DateTime DateEx { get; set; }

			/// <summary>
			/// Inicialization examen
			/// </summary>
			/// <param name="examenName">Name exam</param>
			/// <param name="mark">Mark exam</param>
			/// <param name="dateEx">Date exam</param>
			public Examen(string examenName, int mark, DateTime dateEx)	
			{
				ExamenName = examenName;
				Mark = mark;
				DateEx = dateEx;
			}

			/// <summary>
			/// Override method ToString()
			/// </summary>
			/// <returns>Strok about exam</returns>
			public override string ToString()
			{
				return String.Concat(ExamenName, " ", DateEx.ToShortDateString(), " ", Mark.ToString());
			}
			/// <summary>
			/// Override method equals
			/// </summary>
			/// <param name="obj">Object for compare</param>
			/// <returns>true or false</returns>
			public override bool Equals(object obj)
			{
				if (obj == null) return false;

				Examen ex = (Examen)obj;
				if (ExamenName != ex.ExamenName || DateEx != ex.DateEx || Mark != ex.Mark)
					return false;
				return true;
			}
			/// <summary>
			/// Hash-func
			/// </summary>
			/// <returns>Mark</returns>
			public override int GetHashCode()
			{
				return Mark;
			}

		}

		/// <summary>
		/// Collection examens
		/// </summary>
		private List<Examen> Examens;

		/// <summary>
		/// Getting examen 
		/// </summary>
		/// <param name="index">Index</param>
		/// <returns>Examen</returns>
		public string this[int index]
		{
			get
			{
				return Examens[index].ToString();
			}
		}

		/// <summary>
		/// Name students
		/// </summary>
		public string Name { get; set; }
		/// <summary>
		/// Second name students
		/// </summary>
		public string Surname { get; set; }
		/// <summary>
		/// Patronymic students
		/// </summary>
		public string Patronymic { get; set; }
		/// <summary>
		/// Sex students
		/// </summary>
		public string Sex { get; set; }
		/// <summary>
		/// Date birthday students
		/// </summary>
		public DateTime DateBr { get; set; }

		/// <summary>
		/// Getting Date birthday students for database
		/// </summary>
		/// <returns>Date birthday in view strok</returns>
		public string DateBirt()
		{
			return String.Concat(DateBr.Month.ToString(), "/", DateBr.Day.ToString(), "/", DateBr.Year.ToString());
		}
		/// <summary>
		/// Get name examen by index
		/// </summary>
		/// <param name="i">Index</param>
		/// <returns>Name exam</returns>
		public string ExName(int i)
		{
			return Examens[i].ExamenName;
		}
		/// <summary>
		/// Get mark examen by  index
		/// </summary>
		/// <param name="i">Index</param>
		/// <returns>Mark</returns>
		public int ExMark(int i)
		{
			return Examens[i].Mark;
		}
		/// <summary>
		/// Get date exam by index
		/// </summary>
		/// <param name="i">Index</param>
		/// <returns>String date</returns>
		public string ExDate(int i)
		{
			DateTime date = Examens[i].DateEx;
			return String.Concat(date.Month.ToString(), "/", date.Day.ToString(), "/", date.Year.ToString());
		}
		/// <summary>
		/// Name group student
		/// </summary>
		public string NameGroup { get; set; }
		/// <summary>
		/// Inicialization student
		/// </summary>
		/// <param name="name">Name student</param>
		/// <param name="surname">Second name student</param>
		/// <param name="nameGroup">Group student</param>
		public Student(string surname, string name, string nameGroup)
		{
			NameGroup = nameGroup;
			Examens = new List<Examen>();
			Name = name;
			Surname = surname;
		}

		/// <summary>
		/// Add exam to student
		/// </summary>
		/// <param name="examenName">Name exam</param>
		/// <param name="mark">Mark by exam</param>
		/// <param name="dateEx">Date exam</param>
		public void AddExamen(string examenName, int mark, DateTime dateEx)
		{
			Examens.Add(new Examen(examenName, mark, dateEx));
		}

		/// <summary>
		/// Override method ToString()
		/// </summary>
		/// <returns>Strok about student</returns>
		public override string ToString()
		{
			return String.Concat(Name, " ", Surname, " ", Patronymic, " ",
				Sex, " ", DateBr.ToShortDateString(), " ", NameGroup, " \n", 
				Examens[0].ToString(), "\n", Examens[1].ToString(), "\n", Examens[2].ToString());
		}
		/// <summary>
		/// Override method Equals
		/// </summary>
		/// <param name="obj">Object to compare</param>
		/// <returns>True or false</returns>
		public override bool Equals(object obj)
		{
			if (obj == null)
				return false;

			Student s = (Student)obj;

			if (Name != s.Name || Surname != s.Surname || Patronymic != s.Patronymic
				|| Sex != s.Sex || DateBr != s.DateBr || NameGroup != s.NameGroup
				|| Examens.Count != s.Examens.Count)
				return false;

			for (int i = 0; i < Examens.Count; i++)
				if (Examens[i].Equals(s.Examens[i]) == false)
					return false;
			
			return true;
		}
		/// <summary>
		/// Hash-func
		/// </summary>
		/// <returns>Numbers exams</returns>
		public override int GetHashCode()
		{
			return Examens.Count;
		}

	}
}

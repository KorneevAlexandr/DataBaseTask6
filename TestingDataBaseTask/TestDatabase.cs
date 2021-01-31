using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LibraryToSQL;
using System.Collections.Generic;

namespace TestingDataBaseTask
{
	/// <summary>
	/// Testing ADO, CRUD
	/// </summary>
	[TestClass]
	public class TestDatabase
	{
		/// <summary>
		/// Testing work connection
		/// </summary>
		[TestMethod]
		public void TestingConnection()
		{
			CRUD crud = CRUD.Sourse;

			var except = true;
			var actual = crud.Connection;

			Assert.AreEqual(except, actual);
		}

		/// <summary>
		/// Testing read (select query)
		/// </summary>
		[TestMethod]
		public void TestingRead()
		{
			CRUD crud = CRUD.Sourse;

			List<string> except = new List<string>();
			except.Add("ИП-21 ");
			except.Add("ИТИ-22 ");
			except.Add("ИТП-21 ");

			var actual = crud.Read("SELECT DISTINCT StudentGroup FROM [Students]", 1);

			CollectionAssert.AreEqual(except, actual);
		}

		/// <summary>
		/// Testing create object 
		/// </summary>
		[TestMethod]
		public void TestingCreateStudent()
		{
			DAO dao = new DAO();
			Student student = new Student("Марат", "Крышнев", "ИТП-21")
			{
				Patronymic = "Витальевич",
				Sex = "Муж",
				DateBr = new DateTime(2001, 2, 7)
			};
			student.AddExamen("КС", 2, new DateTime(2021, 1, 12));
			student.AddExamen("Матем", 2, new DateTime(2021, 1, 10));
			student.AddExamen("ООП", 2, new DateTime(2021, 1, 16));

			var actual = dao.CreateObject();

			Assert.AreEqual(student, actual[2][0]);
		}

		/// <summary>
		/// Testing method created list about out students
		/// </summary>
		[TestMethod]
		public void TestingCreateListStudentsOut()
		{
			CreateList create = new CreateList();

			string out_student = "Крышнев Марат ИТП-21 2 2 2";
			string[] mas = create.GetOutStudents();

			var except = true;
			var actual = false;

			for (int i = 0; i < mas.Length; i++)
				if (mas[i].Trim() == out_student)
					actual = true;

			Assert.AreEqual(except, actual);
		}

		/// <summary>
		/// Testing method created list about marks by each group
		/// </summary>
		[TestMethod]
		public void TestingCreateListMarksByGroups()
		{
			double[,] grades = new double[6, 3]
			{
				{ 8, 8, 8 }, { 9, 7, 8 }, { 4, 4, 4 },
				{ 2, 2, 2 }, { 2, 2, 2 }, { 7, 8, 5 }
			};

			double sum = 0;
			for (int i = 0; i < 6; i++)
				sum += (grades[i, 0] + grades[i, 1] + grades[i, 2]) / 3;

			sum = Math.Round(sum / 6, 1);

			string str_except = "ИТИ-22 " + sum.ToString() + " 2 8";
			bool except = true;
			bool actual = false;

			CreateList create = new CreateList();

			string[] mas = create.GetResultMark();
			for (int i = 0; i < mas.Length; i++)
				if (mas[i].Trim() == str_except)
					actual = true;

			Assert.AreEqual(except, actual);
		}

	}
}

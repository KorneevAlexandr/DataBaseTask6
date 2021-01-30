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
			CRUD crud = new CRUD();

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
			CRUD crud = new CRUD();

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

	}
}

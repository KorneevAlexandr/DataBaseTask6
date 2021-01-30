using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LibraryToSQL;

namespace TestingDataBaseTask
{
	/// <summary>
	/// Testing classes student-group
	/// </summary>
	[TestClass]
	public class TestingGroupStudent
	{
		/// <summary>
		/// Test cooperation student class and group
		/// </summary>
		[TestMethod]
		public void TestCooperationStudent_Group()
		{
			Group group = new Group("ИТП-21");

			Student student = new Student("Александр", "Корнеев", "ИТП-21")
			{
				Patronymic = "Олегович",
				Sex = "Муж",
				DateBr = new DateTime(2001, 2, 7)
			};
			student.AddExamen("КС", 7, new DateTime(2021, 1, 12));
			student.AddExamen("Матем", 6, new DateTime(2021, 1, 10));
			student.AddExamen("ООП", 8, new DateTime(2021, 1, 16));

			group.AddStudent(student);

			string[] except_ExName = new string[3] { "КС", "Матем", "ООП" };
			string[] except_ExDate = new string[3]
				{ "1/12/2021", "1/10/2021", "1/16/2021" };

			var actual_ExName = group.GetExamens;
			var actual_ExDate = group.GetDatesExamens;

			CollectionAssert.AreEqual(except_ExName, actual_ExName);
			CollectionAssert.AreEqual(except_ExDate, actual_ExDate);
			Assert.AreEqual(group.Numbers, 1);
			Assert.AreEqual(group.NameGroup, student.NameGroup);
		}

		/// <summary>
		/// Test properties student-class
		/// </summary>
		[TestMethod]
		public void TestStudentClass()
		{
			Student student = new Student("Корнеев", "Александр", "ИТП-21")
			{
				Patronymic = "Олегович",
				Sex = "Муж",
				DateBr = new DateTime(2001, 2, 7)
			};
			student.AddExamen("КС", 7, new DateTime(2021, 1, 12));
			student.AddExamen("Матем", 6, new DateTime(2021, 1, 10));
			student.AddExamen("ООП", 8, new DateTime(2021, 1, 16));

			Assert.AreEqual(student.Name, "Александр");
			Assert.AreEqual(student.Surname, "Корнеев");
			Assert.AreEqual(student.Sex, "Муж");
			Assert.AreEqual(student.DateBr, new DateTime(2001, 2, 7));
			Assert.AreEqual(student[0], "КС 12.01.2021 7");
		}

		/// <summary>
		/// Test ToString() method and Equals()
		/// </summary>
		[TestMethod]
		public void TestingToString()
		{
			Group group = new Group("ИТП-21");
			Student student = new Student("Корнеев", "Александр", "ИТП-21")
			{
				Patronymic = "Олегович",
				Sex = "Муж",
				DateBr = new DateTime(2001, 2, 7)
			};
			student.AddExamen("КС", 7, new DateTime(2021, 1, 12));
			student.AddExamen("Матем", 6, new DateTime(2021, 1, 10));
			student.AddExamen("ООП", 8, new DateTime(2021, 1, 16));

			group.AddStudent(student);

			string Sexcept = "Александр Корнеев Олегович Муж 07.02.2001 ИТП-21 " +
				"\nКС 12.01.2021 7\nМатем 10.01.2021 6\nООП 16.01.2021 8";
			string Gexcept = "ИТП-21 1 students";

			Assert.AreEqual(Sexcept, student.ToString());
			Assert.AreEqual(Gexcept, group.ToString());
			Assert.IsFalse(student.Equals(null));
			Assert.IsFalse(group.Equals(new Group("ИТИ-21")));
		}
	}
}

using System;
using System.Collections.Generic;

namespace LibraryToSQL
{
	/// <summary>
	/// Save database in object
	/// </summary>
	public class DAO
	{
		/// <summary>
		/// Object for access to database
		/// </summary>
		private CRUD cRUD;

		/// <summary>
		/// Initialization object
		/// </summary>
		public DAO()
		{
			cRUD = CRUD.Sourse;
		}

		/// <summary>
		/// Method for convertation strok to date
		/// </summary>
		/// <param name="sdate">Strok</param>
		/// <returns>Date</returns>
		private DateTime ConvertToDate(string sdate)
		{
			sdate = sdate.Split(' ')[0];
			return new DateTime(Convert.ToInt32(sdate.Split('.')[2].Trim()),
				Convert.ToInt32(sdate.Split('.')[1]), Convert.ToInt32(sdate.Split('.')[0]));		
		}

		/// <summary>
		/// Create list groups of database
		/// </summary>
		/// <returns>List groups with students</returns>
		public List<Group> CreateObject()
		{
			List<Group> Groups = new List<Group>();
			string[] mas_group = cRUD.Read("SELECT DISTINCT StudentGroup FROM [Students]", 1).ToArray();

			for (int i = 0; i < mas_group.Length; i++)
				Groups.Add(new Group(mas_group[i]));

			string[] mName = cRUD.Read("SELECT StudentName FROM [Students]", 1).ToArray();
			string[] mSurName = cRUD.Read("SELECT SurName FROM [Students]", 1).ToArray();
			string[] mPatronymic = cRUD.Read("SELECT Patronymic FROM [Students]", 1).ToArray();
			string[] mSex = cRUD.Read("SELECT Sex FROM [Students]", 1).ToArray();
			string[] mDateBr = cRUD.Read("SELECT DateBirth FROM [Students]", 1).ToArray();
			string[] mGroup = cRUD.Read("SELECT StudentGroup FROM [Students]", 1).ToArray();

			string[] ExName1 = cRUD.Read("SELECT ExamenName_1 FROM [Students]", 1).ToArray();
			string[] ExDate1 = cRUD.Read("SELECT DateExamen_1 FROM [Students]", 1).ToArray();
			string[] Mark_1 = cRUD.Read("SELECT Mark_1 FROM [Students]", 1).ToArray();
			string[] ExName2 = cRUD.Read("SELECT ExamenName_2 FROM [Students]", 1).ToArray();
			string[] ExDate2 = cRUD.Read("SELECT DateExamen_2 FROM [Students]", 1).ToArray();
			string[] Mark_2 = cRUD.Read("SELECT Mark_2 FROM [Students]", 1).ToArray();
			string[] ExName3 = cRUD.Read("SELECT ExamenName_3 FROM [Students]", 1).ToArray();
			string[] ExDate3 = cRUD.Read("SELECT DateExamen_3 FROM [Students]", 1).ToArray();
			string[] Mark_3 = cRUD.Read("SELECT Mark_3 FROM [Students]", 1).ToArray();

			for (int i = 0; i < Groups.Count; i++)
				for (int j = 0; j < mGroup.Length; j++)
					if (Groups[i].NameGroup.Trim() == mGroup[j].Trim())
					{
						Student student = new Student(mName[j].Trim(), mSurName[j].Trim(), mGroup[j].Trim())
						{
							Patronymic = mPatronymic[j].Trim(),
							Sex = mSex[j].Trim(),
							DateBr = ConvertToDate(mDateBr[j])
						};
						student.AddExamen(ExName1[j].Trim(), Convert.ToInt32(Mark_1[j]),
							ConvertToDate(ExDate1[j]));
						student.AddExamen(ExName2[j].Trim(), Convert.ToInt32(Mark_2[j]),
							ConvertToDate(ExDate2[j]));
						student.AddExamen(ExName3[j].Trim(), Convert.ToInt32(Mark_3[j]),
							ConvertToDate(ExDate3[j]));

						Groups[i].AddStudent(student);
					}

			return Groups;
		}	

	}
}

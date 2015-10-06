using System;
using Xamarin.Forms;
using School.Core;
using School.Droid;
using System.IO;


namespace School.Droid
{
	public class SQLite_Android
	{
		public SQLite_Android () {}
		public static SQLite.SQLiteConnection GetConnection () {
			var sqliteFilename = "DBSchoolV27.db3";
			string documentsPath = System.Environment.GetFolderPath (System.Environment.SpecialFolder.Personal); // Documents folder
			var path = Path.Combine(documentsPath, sqliteFilename);
			// Create the connection
			var conn = new SQLite.SQLiteConnection(path);
			// Return the database connection
			return conn;
		}
}
}

using System;
using System.IO;

namespace School.iOS
{
	public class SQLite_iOS
	{
		public SQLite_iOS ()
		{
		}
		public static SQLite.SQLiteConnection GetConnection ()
		{
			var sqliteFilename = "SchoolDBV23.db3";
			string documentsPath = Environment.GetFolderPath (Environment.SpecialFolder.Personal); // Documents folder
			string libraryPath = Path.Combine (documentsPath, "..", "Library"); // Library folder
			var path = Path.Combine(libraryPath, sqliteFilename);
			// Create the connection
			var conn = new SQLite.SQLiteConnection(path);
			// Return the database connection
			return conn;
		}
	}
}


using System;
using SQLite;
namespace School.Core
{
	public  interface  ISQLite
	{
		SQLiteConnection GetConnection();
	}
}


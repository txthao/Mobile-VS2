using System;
using System.Collections.Generic;

using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using SQLite;
using System.Net.Http;

namespace School.Core
{
	public class BUser
	{
		public static User GetUser(SQLiteConnection connection,string id )
		{
			DataProvider dtb = new DataProvider (connection);
			return dtb.GetUser (id);
		}
		public static int AddUser(SQLiteConnection connection,User user)
		{
			DataProvider dtb = new DataProvider (connection);
			if (dtb.GetUser (user.Id) == null) {
				return dtb.AddUser (user);
			}
			return 0;
		}
		public static bool IsLogined(SQLiteConnection connection)
		{
			DataProvider dtb = new DataProvider (connection);
			if (dtb.GetMainUser () != null)
				return true;
			return false;
		}
		public static User GetMainUser(SQLiteConnection connection)
		{
			DataProvider dtb = new DataProvider (connection);
			return dtb.GetMainUser ();
		}

		public static void LogOut(SQLiteConnection connection)
		{
			DataProvider dtb = new DataProvider (connection);
			dtb.DeleteAll ();
		}
		public static async Task<bool> CheckAuth(string id, string pass,SQLiteConnection connection)
		{
			var httpClient = new HttpClient ();
			Task<string> contentsTask = httpClient.GetStringAsync ("http://www.schoolapi.somee.com/dangnhap/"+id+"/"+pass);
			string contents =  await contentsTask;
			if (contents.Contains("false"))

				return false;
			User usr = new User ();
			usr.Password = pass;
			usr.Id = id;
			Task<string> contentNameTask = httpClient.GetStringAsync ("http://www.schoolapi.somee.com/api/user/" + id);
			contents=await contentNameTask;
			XDocument doc = XDocument.Parse (contents);
			usr.Hoten= doc.Root.Elements().ElementAt(0).Elements().ElementAt(1).Value.ToString();
			int i = AddUser (connection, usr);
			return true;
		}
	
	}
}


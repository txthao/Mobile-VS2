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
		public static async Task<Exception> CheckAuth(string id, string pass,SQLiteConnection connection)
		{
			pass = base64Encode (pass);
			var httpClient = new HttpClient ();
			Exception  error;
			httpClient.Timeout = TimeSpan.FromSeconds (20);
			string contents;
			Task<string> contentsTask = httpClient.GetStringAsync ("http://www.schoolapi.somee.com/dangnhap/"+id+"/"+pass);

			try
			{
				contents =  await contentsTask;

			}
			catch(Exception e) {
				error =new Exception("Xảy Ra Lỗi Trong Quá Trình Kết Nối Server");
				return error;
			}
			if (contents.Contains ("false")) {
				error=new Exception("Mã Sinh Viên Hoặc Mật Khẩu Không Đúng");
				return error;

			}
			User usr = new User ();
			usr.Password = pass;
			usr.Id = id;
			Task<string> contentNameTask = httpClient.GetStringAsync ("http://www.schoolapi.somee.com/user/" + id);
			contents=await contentNameTask;
			XDocument doc = XDocument.Parse (contents);
			usr.Hoten= doc.Root.Elements().ElementAt(0).Elements().ElementAt(1).Value.ToString();
			int i = AddUser (connection, usr);
			return null;
		}

		public static string base64Encode(string plainText) {
			var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
			return System.Convert.ToBase64String(plainTextBytes);
		}

	}
}
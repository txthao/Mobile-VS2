using System;
using School.Core;

namespace School.iOS
{
	public class ApiHelper
	{
		public static async void Login(string masv,string pass, string token,Action<Exception> callback)
		{
			try 
			{
				Exception error= null;
				if (error==null)
				{
					if (await BUser.CheckAuth(masv,pass,SQLite_iOS.GetConnection()))
					{
							callback(error);
					}
					else
					{
							error=new Exception("Mã Sinh Viên Hoặc Mật Khẩu Không Đúng");
					}
						if (callback!=null&&error!=null) {
							callback(error);
						}
				}
	 		}
			catch {};
			
		}
	}

}


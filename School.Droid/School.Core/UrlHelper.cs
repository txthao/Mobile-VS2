using System;

namespace School.Core
{
	public class UrlHelper
	{
		
		public static string UrlHP(string id, string pass)
		{
			return String.Format ("http://www.schoolapi.somee.com/api/hocphi/{0}/{1}", id, pass);
		}
		public static string UrlDT(string id)
		{
			return String.Format ("http://www.schoolapi.somee.com/api/diemthi/" + id);
		}
		public static string UrlLT(string id)
		{
			return String.Format ("http://www.schoolapi.somee.com/api/lichthi/" + id);
		}
		public static string UrlLH(string id)
		{
			return String.Format ("http://www.schoolapi.somee.com/api/thoikhoabieu/" + id);
		}

	}
}


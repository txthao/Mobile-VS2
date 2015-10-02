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

namespace School.Core
{
	public class BMonHoc
	{
		public static MonHoc GetMH(SQLiteConnection connection,string mamh)
		{
			DataProvider dtb = new DataProvider (connection);
			return dtb.GetMonHoc (mamh);
		}
		public static int Add(SQLiteConnection connection,MonHoc mh)
		{
			DataProvider dtb = new DataProvider (connection);
			if (dtb.GetMonHoc (mh.MaMH) == null) {
				return dtb.AddMH (mh);
			}
			return 0;
		}
		public static int UpdateMH(SQLiteConnection connection,MonHoc mh)
		{
			try
			{
				
				DataProvider dtb = new DataProvider (connection);
				return dtb.UpdateMH(mh);

			}
			catch
			{
				return 0;
			}

		}
	}
}


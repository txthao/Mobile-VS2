using System;
using System.Collections.Generic;

using SQLite;
using System.Xml;
using System.Net;
using System.Xml.Linq;
using System.Linq;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace School.Core
{
   public class BLichThi
    {
       public static List<LichThi> list;
		public static List<LichThi> getAll(SQLiteConnection connection)
        {
			list = new List<LichThi>();
			DataProvider dtb = new DataProvider (connection);
			list = dtb.GetAllLT ();
            return list;
        }
		public static void AddLT(LichThi lt,SQLiteConnection connection )
		{
			DataProvider dtb = new DataProvider (connection);
			if (dtb.GetLT (lt.MaMH) == null) {
				dtb.AddLT (lt);
			}
		}
//		public static LichThi GetNewestLT(LichThi lt,SQLiteConnection connection )
//		{
//			DataProvider dtb = new DataProvider (connection);
//			return dtb.GetNewestLT ();
//		}
			
		public static async Task<List<LichThi>> MakeDataFromXml(SQLiteConnection connection)
		{
			if (BUser.GetMainUser (connection) != null) {
				list = new List<LichThi> ();
				var httpClient = new HttpClient ();
				Task<string> contentsTask = httpClient.GetStringAsync (UrlHelper.UrlLT(BUser.GetMainUser(connection).Id));
				string contents = await contentsTask;
				XDocument doc = XDocument.Parse (contents);

				//get lichthi 
				IEnumerable<XElement> childList =
					from el in doc.Root.Elements ()
					select el;
				//get attri lichthi
				foreach (XElement node in childList) {
				
					LichThi lt = new LichThi ();
					lt.GhepThi = node.Elements ().ElementAt (0).Value.Trim ();
					lt.GioBD = node.Elements ().ElementAt (1).Value.Trim ();
					MonHoc mh = new MonHoc ();
					lt.MaMH = node.Elements ().ElementAt (3).Value.Trim ();
					mh.MaMH = lt.MaMH;
					mh.TenMH = node.Elements ().ElementAt (8).Value.Trim ();
					lt.NgayThi = node.Elements ().ElementAt (4).Value.Trim ();
					lt.PhongThi = node.Elements ().ElementAt (5).Value.Trim ();
					lt.SoLuong = int.Parse (node.Elements ().ElementAt (6).Value.Trim ());
					lt.SoPhut = int.Parse (node.Elements ().ElementAt (7).Value.Trim ());
					lt.ToThi = node.Elements ().ElementAt (9).Value.Trim ();
					list.Add (lt);
					BMonHoc.Add (connection, mh);
					AddLT (lt, connection);
				}
				return list;
			}return null;
		}



		public static List<LichThi> GetLichThiByTime(SQLiteConnection connection)
		{
			DateTime dt= DateTime.Today;
			string time=String.Format("{0}/{1}/{2}",dt.Day,dt.Month,dt.Year);
			DataProvider dtb = new DataProvider (connection);
			return dtb.GetLichThiByTime (time);

		}

    }
}

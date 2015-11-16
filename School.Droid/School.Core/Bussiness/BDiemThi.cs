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
	public class BDiemThi
	{
		
		public static List<DiemThi> list;

		public static List<DiemThi> getAll (SQLiteConnection connection)
		{
			list = new List<DiemThi> ();
			DataProvider dtb = new DataProvider (connection);
			list = dtb.GetAllDT ();
			return list;
		}

		public static int Add (DiemThi lt, SQLiteConnection connection)
		{
			DataProvider dtb = new DataProvider (connection);
			if (dtb.GetDT (lt.Hocky, lt.NamHoc) == null) {
				return dtb.AddDT (lt);
			}
			return 0;
		}

		public static int AddDM (DiemMon dm, SQLiteConnection connection)
		{
			DataProvider dtb = new DataProvider (connection);
			if (dtb.GetDM (dm) == null) {
				return dtb.AddDM (dm);
			}
			return 0;
		}

		public static List<DiemMon> GetAllDiemMon (SQLiteConnection connection)
		{
			DataProvider dtb = new DataProvider (connection);
			return dtb.GetAllDiemMon ();
		}

		public static List<DiemMon> GetDiemMons (SQLiteConnection connection, string hocky, string namhoc)
		{
			DataProvider dtb = new DataProvider (connection);
			return dtb.GetDMs (hocky, namhoc);
		}

		public static DiemThi GetDT (SQLiteConnection connection, string hocky, string namhoc)
		{
			DataProvider dtb = new DataProvider (connection);
			return dtb.GetDT (hocky,namhoc);
		}


		public static DiemThi GetNewestDT (SQLiteConnection connection)
		{
			DataProvider dtb = new DataProvider (connection);
			return dtb.GetNewestDT ();
		}


		public static async  Task<List<DiemThi>> MakeDataFromXml (SQLiteConnection connection)
		{
			if (BUser.GetMainUser (connection) != null) {
				list = new List<DiemThi> ();

				var httpClient = new HttpClient ();
				httpClient.Timeout = TimeSpan.FromSeconds (30);
				string contents;
				Task<string> contentsTask = httpClient.GetStringAsync (UrlHelper.UrlDT(BUser.GetMainUser(connection).Id));
				try{
					contents = await contentsTask;
				}
				catch {
					return null;
				}
				XDocument doc = XDocument.Parse (contents);
				//get lichthi 
				IEnumerable<XElement> childList =
					from el in doc.Root.Elements ()
					select el;
				//get attri lichthi

				foreach (XElement node in childList) {
					DiemThi lt = new DiemThi ();
					lt.DiemRL = node.Elements ().ElementAt (0).Value.Trim ();

					foreach (XElement nod in node.Elements().ElementAt(5).Elements()) {
						DiemMon dm = new DiemMon ();
						MonHoc mh = new MonHoc ();
						dm.Hocky = node.Elements ().ElementAt (9).Value.Trim () [7].ToString ();
						dm.NamHoc = node.Elements ().ElementAt (9).Value.Trim ().Substring (17);
						dm.DiemKT = nod.Elements ().ElementAt (0).Value.Trim ();
						dm.MaMH = nod.Elements ().ElementAt (1).Value.Trim ();
						mh.MaMH = dm.MaMH;
						mh.TenMH = nod.Elements ().ElementAt (5).Value.Trim ();
						mh.SoTC = int.Parse (nod.Elements ().ElementAt (4).Value.Trim ());
						mh.TiLeThi = int.Parse (nod.Elements ().ElementAt (3).Value.Trim ());
						dm.DiemThi = nod.Elements ().ElementAt (6).Value.Trim ();
						dm.DiemTK10 = nod.Elements ().ElementAt (7).Value.Trim ();
						dm.DiemChu = nod.Elements ().ElementAt (8).Value.Trim ();

						AddDM (dm, connection);
						BMonHoc.Add (connection, mh);
						BMonHoc.UpdateMH (connection, mh);
					}
					lt.DiemTB4 = node.Elements ().ElementAt (1).Value.Trim ();
					lt.DiemTB10 = node.Elements ().ElementAt (2).Value.Trim ();
					lt.DiemTBTL4 = node.Elements ().ElementAt (3).Value.Trim ();
					lt.DiemTBTL10 = node.Elements ().ElementAt (4).Value.Trim ();
					lt.LoaiRL = node.Elements ().ElementAt (6).Value.Trim ();
					lt.SoTCDat = node.Elements ().ElementAt (7).Value.Trim ();
					lt.SoTCTL = node.Elements ().ElementAt (8).Value.Trim ();  
					lt.NamHoc = node.Elements ().ElementAt (9).Value.Trim ().Substring (17);
					lt.Hocky = node.Elements ().ElementAt (9).Value.Trim () [7].ToString ();

					Add (lt, connection);
					list.Add (lt);


				}
				return list;
			}
			return null;
		}


	}
}

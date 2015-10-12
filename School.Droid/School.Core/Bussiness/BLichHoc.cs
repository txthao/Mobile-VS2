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
using System.Globalization;

namespace School.Core
{
	public class BLichHoc
	{
		public static List<LichHoc> list;

		public static List<LichHoc> GetAll (SQLiteConnection connection)
		{
			list = new List<LichHoc> ();
			DataProvider dtb = new DataProvider (connection);
			list = dtb.GetAllLH ();
			return list;
		}

		public static void AddLH (LichHoc lt, SQLiteConnection connection)
		{
			DataProvider dtb = new DataProvider (connection);
			if (dtb.GetLH_Ma (lt.MaMH) == null) {
				dtb.AddLH (lt);
			}
		}

		public static LichHoc GetLH (SQLiteConnection connection, string id)
		{
			DataProvider dtb = new DataProvider (connection);
			return dtb.GetLH_Id (id);
		}

		public static int GetId (SQLiteConnection connection)
		{
			DataProvider dtb = new DataProvider (connection);
			if (dtb.GetLastLH () == null) {
				return 0;
			}
			string k = dtb.GetLastLH ().Id;
			return int.Parse (k);
		}

		public static LichHoc GetLast (SQLiteConnection connection)
		{
			DataProvider dtb = new DataProvider (connection);
			return dtb.GetLastLH ();
		}

		public static void AddCTLH (SQLiteConnection connection, chiTietLH ct)
		{
			DataProvider dtb = new DataProvider (connection);
			if (dtb.checkCTLH (ct) == null) {
				dtb.AddCTLH (ct);
			}

		}

		public static List<chiTietLH> GetCTLH (SQLiteConnection connection, string id)
		{
			DataProvider dtb = new DataProvider (connection);
			return dtb.GetCTLH (id);
		}

		public static List<LichHoc> GetNewestLH(SQLiteConnection connection){
			DataProvider dtb = new DataProvider (connection);
			return dtb.GetNewestLH ();
		}

		public static List<LichHoc> GetLH_Time(SQLiteConnection connection,string hocKy, string namHoc){
			DataProvider dtb = new DataProvider (connection);
			return dtb.GetLH_Time (hocKy,namHoc);
		}

		public static async Task<List<LichHoc>> MakeDataFromXml (SQLiteConnection connection)
		{
			if (BUser.GetMainUser (connection) != null) {
				list = new List<LichHoc> ();
				var httpClient = new HttpClient ();
				Task<string> contentsTask = httpClient.GetStringAsync (UrlHelper.UrlLH(BUser.GetMainUser(connection).Id));
				string contents = await contentsTask;
				XDocument doc = XDocument.Parse (contents);
				//get lichthi 
				IEnumerable<XElement> childList =
					from el in doc.Root.Elements ()
					select el;
				//get attri lichthi
				int k = GetId (connection);
				DataProvider dtb = new DataProvider (connection);
				foreach (XElement node in childList) {
				
					LichHoc lichhoc = new LichHoc ();
					lichhoc = dtb.GetLH_Ma (node.Elements ().ElementAt (3).Value.Trim ());

					if (lichhoc != null) {
						chiTietLH ct = new chiTietLH ();
						ct.Id = lichhoc.Id;
						ct.CBGD = node.Elements ().ElementAt (0).Value.Trim ();
						ct.Phong = node.Elements ().ElementAt (6).Value.Trim ();
						ct.Thu = node.Elements ().ElementAt (11).Value.Trim ();
						ct.TietBatDau = node.Elements ().ElementAt (12).Value.Trim ();
						ct.SoTiet = node.Elements ().ElementAt (8).Value.Trim ();
						ct.ThoigianBD = node.Elements ().ElementAt (10).Value.Trim ().Substring (0, 10);
						ct.ThoigianKT = node.Elements ().ElementAt (10).Value.Trim ().Substring (12);
						ct.Tuan = convertToDate (setTKB_Tuan (node.Elements ().ElementAt (13).Value.Trim ()),ct.ThoigianBD);
						AddCTLH (connection, ct);
					} else {
						k++;
						LichHoc lh = new LichHoc ();
						MonHoc mh = new MonHoc ();

						lh.Id = k.ToString ();
						lh.MaMH = node.Elements ().ElementAt (3).Value.Trim ();
						lh.MaLop = node.Elements ().ElementAt (2).Value.Trim ();
						lh.NhomMH = node.Elements ().ElementAt (5).Value.Trim ();
						lh.HocKy = node.Elements ().ElementAt (1).Value.Trim ();
						lh.NamHoc = node.Elements ().ElementAt (4).Value.Trim ();
						mh.MaMH = lh.MaMH;
						mh.TenMH = node.Elements ().ElementAt (9).Value.Trim ();
						mh.SoTC = int.Parse (node.Elements ().ElementAt (7).Value.Trim ());

						chiTietLH ct = new chiTietLH ();
						ct.Id = k.ToString ();
						ct.CBGD = node.Elements ().ElementAt (0).Value.Trim ();
						ct.Phong = node.Elements ().ElementAt (6).Value.Trim ();
						ct.Thu = node.Elements ().ElementAt (11).Value.Trim ();
						ct.TietBatDau = node.Elements ().ElementAt (12).Value.Trim ();
						ct.SoTiet = node.Elements ().ElementAt (8).Value.Trim ();
						ct.ThoigianBD = node.Elements ().ElementAt (10).Value.Trim ().Substring (0, 10);
						ct.ThoigianKT = node.Elements ().ElementAt (10).Value.Trim ().Substring (12);
						ct.Tuan = convertToDate (setTKB_Tuan (node.Elements ().ElementAt (13).Value.Trim ()),ct.ThoigianBD);
						AddCTLH (connection, ct);
						list.Add (lh);
						AddLH (lh, connection);
						BMonHoc.Add (connection, mh);
					
					}


				}
				return list;
			}
			return null;
		}

		public static string convertToDate(List<string> text, string date)
		{


			DateTime firstDate = DateTime.ParseExact (date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
			string s = firstDate.ToString("MM/dd/yyyy").Replace("-", "/");
			for (int i = 1; i < text.Count; i++)
			{
				firstDate = firstDate.AddDays ((int.Parse (text [i]) - int.Parse (text [i - 1])) * 7);
				s+= firstDate.ToString("MM/dd/yyyy").Replace("-", "/");


			}
			return s;
		}

		public static List<string> setTKB_Tuan(string text)
		{
			List<string> result = new List<string>();

			bool flag = false;
			for (int i = 0; i < text.ToCharArray().Length; i++)
			{
				if (i == 0 && flag == false)
				{
					result.Add(text.ToCharArray()[i].ToString());
				}
				else if (flag == true)
				{

					result.Add("1" + text.Substring(i, 1));

				}
				else if (text.ToCharArray()[i] == '0' || text.ToCharArray()[i] == '1')
				{

					result.Add("1" + text.Substring(i, 1));
					flag = true;
				}
				else
				{
					result.Add(text.Substring(i, 1));
				}



			}
			return result;

		}
		public static List<chiTietLH> GetCTLHNow(SQLiteConnection connection)
		{
			
			DataProvider dtb = new DataProvider (connection);
			return dtb.GetCTLichHocByTime ();
		}


	}
}


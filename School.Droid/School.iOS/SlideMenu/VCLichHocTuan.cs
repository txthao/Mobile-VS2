
using System;

using Foundation;
using UIKit;
using School.Core;
using System.Collections.Generic;
using System.Globalization;

namespace School.iOS
{
	public partial class VCLichHocTuan : UIViewController
	{
		List<chiTietLH> listCT;
		string begining;
		string end;
		public VCLichHocTuan () : base ("VCLichHocTuan", null)
		{
		}

		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			progress.Hidden = true;
			LoadData_Tuan (DateTime.Today);
		
			// Perform any additional setup after loading the view, typically from a nib.
		}




		public async void LoadData_Tuan (DateTime dateOfWeek)
		{
			progress.Hidden = false;
			progress.StartAnimating ();
			bool sync = SettingsHelper.LoadSetting ("AutoUpdate"); 
			List<LichHoc> listLH = new List<LichHoc> ();
			if (sync) {
				UIApplication.SharedApplication.NetworkActivityIndicatorVisible = true;
				var newlistlh = BLichHoc.MakeDataFromXml (SQLite_iOS.GetConnection ());
				List<LichHoc> newListLH= await newlistlh;
				UIApplication.SharedApplication.NetworkActivityIndicatorVisible = false;
			}
			progress.StopAnimating ();
			listLH = BLichHoc.GetNewestLH (SQLite_iOS.GetConnection ());
			listCT = new List<chiTietLH> ();
			foreach (var item in listLH) {
				List<chiTietLH> list = BLichHoc.GetCTLH (SQLite_iOS.GetConnection (), item.Id);
				foreach (var ct in list) {
					String result = checkTuan (ct.Tuan, dateOfWeek);
					if (result != "") {
						ct.Tuan = result;
						listCT.Add (ct);
						break;
					}

				}

			}

			GetWeek (dateOfWeek, out begining, out end);

			txtngayLHTuan.Text = "Từ " + begining + " Đến " + end;
			timeLHTuan.Text = "Học Kỳ " + listLH [0].HocKy + " Năm học " + listLH [0].NamHoc;
			listContent.Source = new LichHocTSource (listCT,this);
			listContent.ReloadData ();
		}
		public static DateTime convertFromStringToDate (string date)
		{
			return DateTime.ParseExact (date, "dd/MM/yyyy", CultureInfo.InvariantCulture);

		}

		public static string convertFromDateToString (DateTime date)
		{
			return date.ToString ("dd/MM/yyyy");
		}
		partial void btTuanKeClick (NSObject sender)
		{
			listContent.Source=null;
			listContent.ReloadData();
			LoadData_Tuan (convertFromStringToDate (begining).AddDays (7));
		}
		partial void btTuanTRCClick (NSObject sender)
		{
			listContent.Source=null;
			listContent.ReloadData();
			LoadData_Tuan (convertFromStringToDate (begining).AddDays (-7));
		}
		public static string checkTuan (string s, DateTime dayOfWeek)
		{
			int number = s.ToCharArray ().Length / 10;
			for (int i = 1; i <= number; i++) {
				string a = s.Substring ((i - 1) * 10, 10);
				if (DateInsideOneWeek (Convert.ToDateTime (a), dayOfWeek)) {
					return a;
				}
			}
			return "";
		}

		public static bool DateInsideOneWeek (DateTime checkDate, DateTime referenceDate)
		{
			// get first day of week from your actual culture info, 
			DayOfWeek firstWeekDay = DayOfWeek.Monday;
			// or you can set exactly what you want: firstWeekDay = DayOfWeek.Monday;
			// calculate first day of week from your reference date
			DateTime startDateOfWeek = referenceDate;
			while (startDateOfWeek.DayOfWeek != firstWeekDay) {
				startDateOfWeek = startDateOfWeek.AddDays (-1d);
			}
			// fist day of week is find, then find last day of reference week
			DateTime endDateOfWeek = startDateOfWeek.AddDays (6d);
			// and check if checkDate is inside this period
			return checkDate >= startDateOfWeek && checkDate <= endDateOfWeek;
		}
		private static void GetWeek (DateTime now, out string begining, out string end)
		{
			CultureInfo cultureInfo = CultureInfo.CurrentCulture;
			var firstDayOfWeek = DayOfWeek.Monday;
			int offset = firstDayOfWeek - now.DayOfWeek;
			if (offset != 1) {
				DateTime weekStart = now.AddDays (offset);
				DateTime endOfWeek = weekStart.AddDays (6);
				begining = convertFromDateToString (weekStart);
				end = convertFromDateToString (endOfWeek);
			} else {
				begining = convertFromDateToString (now.AddDays (-6));
				end = convertFromDateToString (now);
			}
		}
	}
}


using System;
using System.Globalization;
using Android.Net;
using Android.Content;
using School.Core;
using System.Threading.Tasks;
using System.Collections.Generic;
using Android.App;
using Android.OS;
using Android.Widget;

namespace School.Droid
{
	public class Common
	{
		public static string checkSpaceValue (string text)
		{
			
			if (text.Equals ("&nbsp;")) {
				return "";
			}
			return text;
		}

		public static string calPercent (int tiLe)
		{


			return (100 - tiLe).ToString () + "/" + tiLe;
		}


		public static void calSemester (string hk, string nh, int number, out string outHK, out string outNH)
		{
			if ("1".Equals (hk) && (-1).Equals (number)) {
				outHK = "3";
				outNH = (int.Parse (nh) + number).ToString ();
			} else if ("3".Equals (hk) && 1.Equals (number)) {
				outHK = "1";
				outNH = (int.Parse (nh) + number).ToString ();
			} else {
				outHK = (int.Parse (hk) + number).ToString ();
				outNH = nh;
			}
		}
		public static List<string>  strListTuanToArrayString(string s)
		{
			
			int number = s.ToCharArray ().Length / 10;
			List<string> strs = new List<string> ();
			for (int i = 1; i <= number; i++) {
				string a = s.Substring ((i - 1) * 10, 10);
				strs.Add (a);
			}
			return strs;
		}
		public static string setDiemTK (string diem10, string diemChu)
		{
			if (!diem10.Equals ("&nbsp;") && !diemChu.Equals ("&nbsp;")) {
				return diem10 + "(" + diemChu + ")";		
			}
			return "";
		}
		public static bool checkNWConnection(Context ct)
		{
			ConnectivityManager connectivityManager = (ConnectivityManager)ct.GetSystemService (Context.ConnectivityService);
			NetworkInfo activeConnection = connectivityManager.ActiveNetworkInfo;
			bool isOnline = (activeConnection != null) && activeConnection.IsConnected;
			return isOnline;
		}
		public static async Task<string> LoadDataFromSV(Context ctx)
		{
			try
			{
			var newlistlh= BLichHoc.MakeDataFromXml (SQLite_Android.GetConnection ());
			List<LichHoc> newListLH= await newlistlh;
			var newlistlt= BLichThi.MakeDataFromXml (SQLite_Android.GetConnection ());
			List<LichThi> newListLT= await newlistlt;
			var dtKS=await BDiemThi.MakeDataFromXml (SQLite_Android.GetConnection ());
			var hpKS= await BHocPhi.MakeDataFromXml (SQLite_Android.GetConnection ());
			try{
					DrawerActivity drAc=(DrawerActivity)ctx;
					drAc.SelectItem(drAc.previousItemChecked);
			}catch
			{
			}
			var prefs = Application.Context.GetSharedPreferences("SGU APP", FileCreationMode.Private);              
			var checkRemind = prefs.GetBoolean ("Remind",false);
			if ( checkRemind)
			{
			ScheduleReminder reminder = new ScheduleReminder(ctx);
					if (newListLH!=null) await reminder.RemindAllLH(newListLH);
					if (newListLT!=null) await reminder.RemindAllLT(newListLT);

			Toast.MakeText (ctx, "Cài đặt nhắc lịch cho dữ liệu mới thành công", ToastLength.Long).Show();
			}
			if (dtKS!=null&&hpKS!=null&&newListLH!=null&&newListLT!=null)
			{
					Toast.MakeText (ctx, "Cập nhật dữ liệu thành công", ToastLength.Long).Show();
					return "load success ";
			}	
				else
			{
				Toast.MakeText (ctx, "Xảy ra lỗi trong quá trình tải dữ liệu", ToastLength.Long).Show();
					return "load failed ";
			}

			
			
			}
			catch {
				Toast.MakeText (ctx, "Xảy ra lỗi trong quá trình tải dữ liệu, vui lòng thử lại sau", ToastLength.Long).Show();
				return "load failed";
			}



		}
		public static Bundle LoadSettings()
		{
			var prefs = Application.Context.GetSharedPreferences("SGU APP", FileCreationMode.Private);              
			var checkRemind = prefs.GetBoolean ("Remind",false);
			var autoUpdate= prefs.GetBoolean ("AutoUpdateData",false);

			var bundle = new Bundle();

			bundle.PutBoolean ("Remind", checkRemind);
			bundle.PutBoolean ("AutoUpdateData", autoUpdate);
			return bundle;
		}
		// s= list of date in ctlH 
		public static bool checkInWeek (string s, string date)
		{
			int number = s.ToCharArray ().Length / 10;
			for (int i = 1; i <= number; i++) {
				string a = s.Substring ((i - 1) * 10, 10);
				if (a.Equals (date)) {
					return true;
				}
			}
			return false;
		}


	}
}


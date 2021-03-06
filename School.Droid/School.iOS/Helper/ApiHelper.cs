﻿using System;
using School.Core;
using System.Threading.Tasks;
using System.Collections.Generic;
using UIKit;

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
					error=await BUser.CheckAuth(masv,pass,SQLite_iOS.GetConnection());
					if (error==null)
					{
						await ApiHelper.LoadDataFromSV(VCLogIn.Instance);
							callback(error);
					}
					else
					{
						
					}
						if (callback!=null&&error!=null) {
							callback(error);
						}
				}
	 		}
			catch {};
			
		}
		public async static void LogOut()
		{
			bool accep = await LayoutHelper.ShowAlert ("Chú ý", "Bạn có muốn đăng xuất khỏi ứng dụng?");
			if (accep) {
				BUser.LogOut (SQLite_iOS.GetConnection ());
				SettingsHelper.SaveSetting ("AutoUpdate", false);
				SettingsHelper.SaveSetting ("Remind", false);
				RootViewController.Instance.doLogOut ();
			} else {
				RootViewController.Instance.navigation.SelectedIndex = 1;
			}
		}
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

		public static async Task<string> LoadDataFromSV(UIViewController controller)
		{
			try
			{
				var newlistlh= BLichHoc.MakeDataFromXml (SQLite_iOS.GetConnection ());
				List<LichHoc> newListLH= await newlistlh;
				var newlistlt= BLichThi.MakeDataFromXml (SQLite_iOS.GetConnection ());
				List<LichThi> newListLT= await newlistlt;
				var dtRS=await BDiemThi.MakeDataFromXml (SQLite_iOS.GetConnection ());
				var hpRS=await BHocPhi.MakeDataFromXml (SQLite_iOS.GetConnection ());
//				var prefs = Application.Context.GetSharedPreferences("SGU APP", FileCreationMode.Private);              
//				var checkRemind = prefs.GetBoolean ("Remind",false);
				var checkRemind=SettingsHelper.LoadSetting("Remind");
				if (checkRemind)
				{
					VCHomeReminder remind= new VCHomeReminder(controller);
					if (newListLH!=null) await remind.RemindALLLH(newListLH,"");
					if (newListLT!=null) await remind.RemindAllLT(newListLT);
				}


				if (dtRS!=null&&hpRS!=null&&newListLH!=null&&newListLT!=null)
				{
					
					return "Cập nhật dữ liệu thành công";
				}	
				else
				{
					
					return "Xảy ra lỗi trong quá trình cập nhật dữ liệu";
				}
			}
			catch {
				return "Xảy ra lỗi trong quá trình cập nhật dữ liệu";
			}



		}
	}


}


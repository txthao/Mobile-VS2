
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using School.Core;
using Android.Content.PM;

namespace School.Droid
{
	[Activity (Label = "Cài đặt nhắc lịch",Icon="@drawable/reminder", ScreenOrientation = ScreenOrientation.Portrait)]			
	public class Remider : Activity
	{
		string MH;
		bool isLHT;
		string ngayhoc;
		string tietBD;
		string soTiet;
		Bundle bundle;
		Boolean check;
		MonHoc mh;
		LichHoc lh;
		LichThi lt;
		EditText edtxt_minutes;
		EditText edtxt_content;
		Button Cancel;
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.Reminder_Dialog);
			Button Save = FindViewById<Button> (Resource.Id.btnSave);
			Button Del = FindViewById<Button> (Resource.Id.btnDel);
			Cancel = FindViewById<Button> (Resource.Id.btnCancel);
			edtxt_content = FindViewById<EditText> (Resource.Id.edtxt_Content);
			TextView title = FindViewById<TextView> (Resource.Id.txtTitleRM);
			TextView subject = FindViewById<TextView> (Resource.Id.txtSubjectRM);
			TextView time = FindViewById<TextView> (Resource.Id.txtTimeRM);
			TextView date = FindViewById<TextView> (Resource.Id.txtDateRM);
			edtxt_minutes = FindViewById<EditText> (Resource.Id.txt_minutes);
			bundle = Intent.GetBundleExtra ("RemindValue");
			MH = bundle.GetString ("MH");
			tietBD = bundle.GetString ("TietBD");
			ngayhoc= bundle.GetString ("NgayHoc");
			soTiet= bundle.GetString ("SoTiet");
			check = bundle.GetBoolean ("check");
			isLHT= bundle.GetBoolean ("isLHT");
			string minutes, mess;
			if (check) {
				string namhoc=bundle.GetString ("NamHoc");
				string hocky=bundle.GetString ("HocKy");
				mh = BMonHoc.GetMH (SQLite_Android.GetConnection (), MH);
				lt = BLichThi.GetLT (SQLite_Android.GetConnection (), MH,namhoc,hocky);
				title.Text = "NHẮC LỊCH THI";
				subject.Text = "Môn:" + mh.TenMH;
				date.Text = " Ngày: " + lt.NgayThi;
				time.Text = "Thời gian: " + lt.GioBD ;
				LTRemindItem item = BRemind.GetLTRemind (SQLite_Android.GetConnection (), lt.MaMH, lt.NamHoc, lt.HocKy);
				if (item != null) {
					ScheduleReminder reminder = new ScheduleReminder (this);
					reminder.GetRemind (item.EventID, out minutes, out mess);
					if (minutes != null || mess != null) {
						edtxt_content.Text = mess;
						edtxt_minutes.Text = minutes;
					}
				}

			} else {
				lh = BLichHoc.GetLH (SQLite_Android.GetConnection (), MH);
				mh = BMonHoc.GetMH (SQLite_Android.GetConnection (), lh.MaMH);
				isLHT = true;
				isLHT= bundle.GetBoolean ("isLHT");
				title.Text = "NHẮC LỊCH HOC";
				subject.Text = "Môn: " + mh.TenMH;

				List<LHRemindItem> list = BRemind.GetLHRemind(SQLite_Android.GetConnection (),lh.Id);
				if (list.Count != 0) {
					ScheduleReminder reminder = new ScheduleReminder (this);
					reminder.GetRemind (list [0].EventID, out minutes, out mess);
					if (minutes != null || mess != null) {
						edtxt_content.Text = mess;
						edtxt_minutes.Text = minutes;
					}
				}
				date.Visibility = ViewStates.Gone;
				time.Visibility = ViewStates.Gone;
				if (isLHT) {
					date.Visibility = ViewStates.Visible;
					time.Visibility = ViewStates.Visible;
					string exNgay = ngayhoc.Substring (3, 2);
					exNgay = exNgay + "/" + ngayhoc.Substring(0,2)+"/"+ngayhoc.Substring(6,4);
					date.Text = " Ngày: " + exNgay;
					time.Text =  "Tiết: " + tietBD ;
					LHRemindItem item = BRemind.GetLHRemind(SQLite_Android.GetConnection (),lh.Id,ngayhoc);
					if (item != null) {
						ScheduleReminder reminder = new ScheduleReminder (this);
						reminder.GetRemind (item.EventID, out minutes, out mess);
						if (minutes != null || mess != null) {
							edtxt_content.Text = mess;
							edtxt_minutes.Text = minutes;
						}
					}
				}


			}
			// 
			Save.Click += Save_Click;
			Cancel.Click += new EventHandler (btnCancel_OnClickListener);
			Del.Click += Del_Click;

		}
		void btnCancel_OnClickListener(object sender, EventArgs e){

			this.Finish ();
		}

		void Del_Click (object sender, EventArgs e)
		{
			ScheduleReminder reminder = new ScheduleReminder (this);
			List<string> listEventId = new List<string> ();
			if (check) {
				LTRemindItem item = BRemind.GetLTRemind (SQLite_Android.GetConnection (), lt.MaMH, lt.NamHoc, lt.HocKy);
				listEventId.Add (item.EventID);
			} else if (isLHT) {
				LHRemindItem item = BRemind.GetLHRemind(SQLite_Android.GetConnection (),lh.Id,ngayhoc);
				listEventId.Add (item.EventID);
			} else {
				List<LHRemindItem> list = BRemind.GetLHRemind(SQLite_Android.GetConnection (),lh.Id);
				foreach (var item in list) {
					listEventId.Add (item.EventID);
				}
			}

			reminder.DeleteRemind (listEventId);
			Toast.MakeText (this, "Xóa nhắc lịch thành công", ToastLength.Long).Show();
			Cancel.CallOnClick ();
		}

		void Save_Click (object sender, EventArgs e)
		{
			ScheduleReminder reminder = new ScheduleReminder (this);
			if (edtxt_content.Text == "") {
				reminder.content = "0";
			}
			else reminder.content = edtxt_content.Text;
			reminder.MinutesRemind = int.Parse(edtxt_minutes.Text.Trim());
			if (check) {


				reminder.lt = lt;
				reminder.SetCalenDarLT ();
			} else {

				reminder.lh = lh;
				if (isLHT) {
					chiTietLH ct = new chiTietLH ();
					ct.Id = lh.Id;
					ct.TietBatDau = tietBD;
					ct.SoTiet = soTiet;
					reminder.DateForCTLH = ngayhoc;
					reminder.ctlh = ct;
					reminder.SetCalenDarLH ();
				} else {
					List<LichHoc> list = new List<LichHoc> ();
					list.Add (lh);
					reminder.RemindAllLH (list);
				}
			}
			Toast.MakeText (this, "Cài đặt nhắc lịch thành công", ToastLength.Long).Show();
			Cancel.CallOnClick ();
		
		}

	}
}


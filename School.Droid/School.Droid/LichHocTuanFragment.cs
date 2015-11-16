
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using System.Xml;
using School.Core;
using System.Globalization;

namespace School.Droid
{
	public class LichHocTuanFragment : Fragment
	{
		ExpandableListView listView_Tuan;
		TextView lbl_HK;
		TextView lbl_TuNgay;
		TextView lbl_DenNgay;
		ProgressBar progress;
		bool check,autoupdate;
		Bundle bundle;
		List<chiTietLH> listCT;

		public override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);
			// Create your fragment here
		}

		public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{

			//Theo Tuan

			var rootView = inflater.Inflate (Resource.Layout.LichHoc_Tuan, container, false);
			listView_Tuan = rootView.FindViewById<ExpandableListView> (Resource.Id.listLH_Tuan);
			listView_Tuan.GroupClick += (sender, e) => {
				listView_ItemClick(sender,e);
			//	return false;
				// row click 


			};
			progress = rootView.FindViewById<ProgressBar> (Resource.Id.progressLHTuan);
			lbl_TuNgay = rootView.FindViewById<TextView> (Resource.Id.lbl_TuNgay);
			lbl_DenNgay = rootView.FindViewById<TextView> (Resource.Id.lbl_DenNgay);
			lbl_HK = rootView.FindViewById<TextView> (Resource.Id.lbl_HK_Tuan);
			LinearLayout linear_ThoiGian= rootView.FindViewById<LinearLayout> (Resource.Id.linear_LH_Tuan_ThoiGian);
			TextView txtNotify = rootView.FindViewById<TextView> (Resource.Id.txtNotify_LH_Tuan);
			RadioGroup radioGroup = rootView.FindViewById<RadioGroup>(Resource.Id.radioGroup2);
			//button 
			Button btnTuanTruoc = rootView.FindViewById<Button> (Resource.Id.btnTuanTruoc);
			Button btnTuanKe = rootView.FindViewById<Button> (Resource.Id.btnTuanKe);
			//bundle
			bundle=this.Arguments;
			check = bundle.GetBoolean ("Remind");
			autoupdate = bundle.GetBoolean ("AutoUpdateData");

			LichHoc lh = BLichHoc.GetLast (SQLite_Android.GetConnection ());
			if (lh != null) {
				btnTuanTruoc.Enabled = true;
				btnTuanKe.Enabled = true;
				radioGroup.Visibility = ViewStates.Visible;
				linear_ThoiGian.Visibility = ViewStates.Visible;
				txtNotify.Visibility = ViewStates.Gone;
				LoadData_Tuan (DateTime.Today);
			}
			else {
				progress.Visibility = ViewStates.Gone;
				btnTuanTruoc.Enabled = false;
				btnTuanKe.Enabled = false;
				radioGroup.Visibility = ViewStates.Gone;
				linear_ThoiGian.Visibility = ViewStates.Gone;
				txtNotify.Visibility = ViewStates.Visible;
				txtNotify.Text = "Hiện tại lịch học chưa có dữ liệu. Xin vui lòng thử lại sau!!!";
			}


			//radio button 
			RadioButton rb_tuan = rootView.FindViewById<RadioButton> (Resource.Id.rb_dangTuan_Tuan);
			RadioButton rb_hocKy = rootView.FindViewById<RadioButton> (Resource.Id.rb_dangHK_Tuan);
			rb_tuan.Checked = true;
			rb_hocKy.Click += new EventHandler (rd_OnCheckedChangeListener);

			// button event
			btnTuanTruoc.Click += new EventHandler (btnTuanTruoc_Click);
			btnTuanKe.Click += new EventHandler (btnTuanKe_Click);



			return rootView;
		}

		void listView_ItemClick(object sender, ExpandableListView.GroupClickEventArgs e){
			// content to pass 
			var bundle1 = new Bundle();
		
			bundle1.PutString ("MH", listCT[e.GroupPosition].Id);
			bundle1.PutString ("TietBD", listCT [e.GroupPosition].TietBatDau);
			bundle1.PutString ("NgayHoc", listCT [e.GroupPosition].Tuan);
			bundle1.PutString ("SoTiet", listCT [e.GroupPosition].SoTiet);
			bundle1.PutBoolean ("check", false);
			bundle1.PutBoolean ("isLHT", true);
			// call fragment
			Intent myintent = new Intent (Activity, typeof(Remider));
			myintent.PutExtra ("RemindValue", bundle1);
			StartActivity (myintent);
		}

		void rd_OnCheckedChangeListener (object sender, EventArgs e)
		{
			LichHocHKFragment fragment = new LichHocHKFragment ();
			fragment.Arguments = bundle;
			FragmentManager.BeginTransaction ()
				.Add (Resource.Id.content_frame, fragment).AddToBackStack("12")
				.Commit ();
		}

		void btnTuanTruoc_Click (object sender, EventArgs e)
		{
			
			LoadData_Tuan (convertFromStringToDate (lbl_TuNgay.Text).AddDays (-7));
		}

		void btnTuanKe_Click (object sender, EventArgs e)
		{
			
			LoadData_Tuan (convertFromStringToDate (lbl_TuNgay.Text).AddDays (7));
		}

		async void LoadData_Tuan (DateTime dateOfWeek)
		{
			listView_Tuan.Visibility = ViewStates.Invisible;
			progress.Visibility = ViewStates.Visible;
			progress.Indeterminate = true;
			List<LichHoc> listLH = new List<LichHoc> ();
			if (Common.checkNWConnection (Activity) == true && autoupdate == true) {
				var newlistlh= BLichHoc.MakeDataFromXml (SQLite_Android.GetConnection ());
				List<LichHoc> newListLH= await newlistlh;
				if (check) {
					ScheduleReminder reminder = new ScheduleReminder (Activity);

					await reminder.RemindAllLH (newListLH);
				}
			}
			listLH = BLichHoc.GetNewestLH (SQLite_Android.GetConnection ());
			listCT = new List<chiTietLH> ();
			foreach (var item in listLH) {
				List<chiTietLH> list = BLichHoc.GetCTLH (SQLite_Android.GetConnection (), item.Id).ToList ();
				foreach (var ct in list) {
					String result = checkTuan (ct.Tuan, dateOfWeek);
					if (result != "") {
						ct.Tuan = result;
						listCT.Add (ct);
						break;
					}

				}

			}
			string begining;
			string end;
			GetWeek (dateOfWeek, out begining, out end);
			lbl_TuNgay.Text = begining;
			lbl_DenNgay.Text = end;
			lbl_HK.Text = "Học Kỳ " + listLH [0].HocKy + " Năm học " + listLH [0].NamHoc;
			listView_Tuan.SetAdapter (new LichHocTuanAdapter (Activity, listCT.OrderBy(x=>x.Thu).ToList())); 
			progress.Indeterminate = false;
			progress.Visibility = ViewStates.Gone;
			listView_Tuan.Visibility = ViewStates.Visible;
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



		public static DateTime convertFromStringToDate (string date)
		{
			return DateTime.ParseExact (date, "dd/MM/yyyy", CultureInfo.InvariantCulture);

		}

		public static string convertFromDateToString (DateTime date)
		{
			return date.ToString ("dd/MM/yyyy");
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

	
	}
}


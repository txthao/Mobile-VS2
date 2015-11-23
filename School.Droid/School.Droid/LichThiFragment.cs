
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
using School.Core;
using System.Xml;

namespace School.Droid
{
	public class LichThiFragment : Fragment
	{
		List<LichThi> list;
		ListView listView;
		ProgressBar progress;
		TextView txtHocKy;
		View rootView;
		LinearLayout linear;
		TextView txtNotify;
		bool check,autoupdate;
		public static LichThiFragment instance;

		public LichThiFragment () 
		{
			instance = this;
		}
		public override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);
		}

		public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			// Use this to return your custom view for this Fragment
			// return inflater.Inflate(Resource.Layout.YourFragment, container, false);
		
  
			rootView = inflater.Inflate(Resource.Layout.LichThi, container, false);
		
			txtHocKy = rootView.FindViewById<TextView> (Resource.Id.txtHocKy);
            listView = rootView.FindViewById<ListView>(Resource.Id.listLT);
			progress= rootView.FindViewById<ProgressBar>(Resource.Id.progressLT);
			linear = rootView.FindViewById<LinearLayout>(Resource.Id.linear1);
			txtNotify = rootView.FindViewById<TextView>(Resource.Id.txtNotify_LT);

			Bundle bundle=this.Arguments;
			check = bundle.GetBoolean ("Remind");
			autoupdate = bundle.GetBoolean ("AutoUpdateData");
			LoadData ();
			listView.ItemLongClick += listView_ItemClick;
			return rootView;
		}
		void listView_ItemClick(object sender, AdapterView.ItemLongClickEventArgs e){
			// content to pass to dialog fragment
			var bundle1 = new Bundle();
			bundle1.PutString ("MH", list[e.Position].MaMH);
			bundle1.PutBoolean ("check", true);
			bundle1.PutString ("NamHoc", list[e.Position].NamHoc);
			bundle1.PutString ("HocKy", list[e.Position].HocKy);
			Intent myintent = new Intent (Activity, typeof(Remider));
			myintent.PutExtra ("RemindValue", bundle1);
			StartActivity (myintent);
		}


		async void LoadData()
		{
			progress.Visibility = ViewStates.Visible;
			progress.Indeterminate = true;
			list = new List<LichThi>();
			if (Common.checkNWConnection (Activity) == true && autoupdate == true) {
				var newlistlt= BLichThi.MakeDataFromXml (SQLite_Android.GetConnection ());
				List<LichThi> newListLT= await newlistlt;
				if (newListLT == null) {
					Toast.MakeText (Activity, "Xảy ra lỗi trong quá trình cập nhật dữ liệu từ server", ToastLength.Long).Show ();
				} else {
					if (check) {
						ScheduleReminder reminder = new ScheduleReminder (Activity);
						await reminder.RemindAllLT (newListLT);
					}
				}
			}
			list = BLichThi.GetNewestLT (SQLite_Android.GetConnection ()); 
			//list = BLichThi.getAll (SQLite_Android.GetConnection ());
			if (list.Count > 0) {
				linear.Visibility = ViewStates.Visible;
				txtNotify.Visibility = ViewStates.Gone;
				LichThiAdapter adapter = new LichThiAdapter (Activity, list);
				rootView.FindViewById<TextView> (Resource.Id.txtHocKy).Text = "Học Kỳ " + list [0].HocKy + " Năm Học " + list [0].NamHoc;
				listView.Adapter = adapter;

			}
			else {
				linear.Visibility = ViewStates.Gone;
				txtNotify.Visibility = ViewStates.Visible;
				txtNotify.Text = "Hiện tại lịch thi chưa có dữ liệu. Xin vui lòng thử lại sau!!!";
			}
			progress.Visibility = ViewStates.Gone;
			progress.Indeterminate = false;

		}
		public static LichThiFragment Instance
		{
			get
			{
				if (instance == null)
					instance = new LichThiFragment ();
				return instance;
			}
		}
	}
}


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


namespace School.Droid
{
	public class LichHocHKFragment : Fragment
	{
		ListView listView_HK;
		TextView lbl_HK;
		TextView lbl_NH;
		ProgressBar progress;
		LinearLayout linear;
		LinearLayout linearLH;
		TextView txtNotify;
		//RadioGroup radioGroup;
		bool check,autoupdate;
		Bundle bundle;
		List<chiTietLH> listCT;
		public static LichHocHKFragment instance;

		public LichHocHKFragment () 
		{
			instance = this;
		}
		public override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);
			// Create your fragment here
		}

		public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			// Use this to return your custom view for this Fragment
			// return inflater.Inflate(Resource.Layout.YourFragment, container, false);

			//	  Lich hoc theo HK
			var rootView = inflater.Inflate (Resource.Layout.LichHoc_HK, container, false);
			listView_HK = rootView.FindViewById<ListView> (Resource.Id.listLH);
			lbl_HK = rootView.FindViewById<TextView> (Resource.Id.lbl_HK_LH);
			lbl_NH = rootView.FindViewById<TextView> (Resource.Id.lbl_NH_LH);
			progress = rootView.FindViewById<ProgressBar> (Resource.Id.progressLH);
			linear = rootView.FindViewById<LinearLayout>(Resource.Id.linear_HK_LH);
			linearLH = rootView.FindViewById<LinearLayout>(Resource.Id.linearLH);
			txtNotify = rootView.FindViewById<TextView>(Resource.Id.txtNotify_LT_HK);
		//	radioGroup = rootView.FindViewById<RadioGroup>(Resource.Id.radioGroup1);
		    bundle=this.Arguments;
			check = bundle.GetBoolean ("Remind");
			autoupdate = bundle.GetBoolean ("AutoUpdateData");

			//load data

			LoadData_HK ();
		
			// row click
			listView_HK.ItemLongClick += listView_ItemClick;
						

			return rootView;
		}

		void listView_ItemClick(object sender, AdapterView.ItemLongClickEventArgs e){
			// content to pass 
			var bundle1 = new Bundle();
			LichHoc lh = BLichHoc.GetLH(SQLite_Android.GetConnection (),listCT[e.Position].Id);

			bundle1.PutString ("MH", lh.Id);
			bundle1.PutBoolean ("isLHT", false);
			bundle1.PutBoolean ("check", false);
			bundle1.PutString ("Thu", listCT [e.Position].Thu);
			bundle1.PutString ("TietBD", listCT [e.Position].TietBatDau);

			Intent myintent = new Intent (Activity, typeof(Remider));
			myintent.PutExtra ("RemindValue", bundle1);
			StartActivity (myintent);
		}
			
		async void LoadData_HK ()
		{
			listView_HK.Visibility = ViewStates.Invisible;
			progress.Visibility = ViewStates.Visible;
			progress.Indeterminate = true;
			List<LichHoc> listLH = new List<LichHoc> ();
			if (Common.checkNWConnection (Activity) == true&&autoupdate==true) {
			
				var newlistlh = BLichHoc.MakeDataFromXml (SQLite_Android.GetConnection ());
				List<LichHoc> newListLH= await newlistlh;
				if (newListLH == null) {
					Toast.MakeText (Activity, "Xảy ra lỗi trong quá trình cập nhật dữ liệu từ server", ToastLength.Long).Show ();
				} else {
					if (check) {
						ScheduleReminder reminder = new ScheduleReminder (Activity);

						await reminder.RemindAllLH (newListLH);
					}
				}
			}
			listLH = BLichHoc.GetNewestLH (SQLite_Android.GetConnection ());
			if (listLH.Count > 0) {
				//radioGroup.Visibility = ViewStates.Visible;
				linearLH.Visibility = ViewStates.Visible;
				linear.Visibility = ViewStates.Visible;
				txtNotify.Visibility = ViewStates.Gone;
				listCT = new List<chiTietLH> ();
				foreach (var item in listLH) {
					listCT.AddRange (BLichHoc.GetCTLH (SQLite_Android.GetConnection (), item.Id));

				}

				lbl_HK.Text = listLH [0].HocKy;
				lbl_NH.Text = listLH [0].NamHoc;
				LichHocHKAdapter adapter = new LichHocHKAdapter (Activity, listCT);
				listView_HK.Adapter = adapter;  
			}else {
				//radioGroup.Visibility= ViewStates.Invisible;
				linearLH.Visibility = ViewStates.Gone;
				linear.Visibility = ViewStates.Gone;
				txtNotify.Visibility = ViewStates.Visible;
				txtNotify.Text = "Hiện tại lịch học chưa có dữ liệu. Xin vui lòng thử lại sau!!!";
			}
			progress.Indeterminate = false;
			progress.Visibility = ViewStates.Gone;
			listView_HK.Visibility = ViewStates.Visible;

		
		}

		public static LichHocHKFragment Instance
		{
			get
			{
				if (instance == null)
					instance = new LichHocHKFragment ();
				return instance;
			}
		}

	}
}


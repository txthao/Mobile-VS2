﻿
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
	public class DiemThiFragment : Fragment
	{
		ListView listView;
		ProgressBar progress;
		LinearLayout linear;
		TextView txtNotify;
		TextView txtHocKyDT;
		bool check,autoupdate;
		Bundle bundle;
		public static DiemThiFragment instance;

		public DiemThiFragment () 
		{
			instance = this;
		}

		public override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);
		}

		public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{

			var rootView = inflater.Inflate(Resource.Layout.DiemThi, container, false);
			bundle = this.Arguments;
			check = bundle.GetBoolean ("Remind");
			autoupdate = bundle.GetBoolean ("AutoUpdateData");

			listView = rootView.FindViewById<ListView>(Resource.Id.listDT);
			progress=rootView.FindViewById<ProgressBar>(Resource.Id.progressDT);
			linear = rootView.FindViewById<LinearLayout>(Resource.Id.linear11);
			txtNotify = rootView.FindViewById<TextView>(Resource.Id.txtNotify_DT);
			txtHocKyDT = rootView.FindViewById<TextView> (Resource.Id.txtHocKyDT);
			LoadData ();

			return rootView;
		}
				
		async void LoadData()
		{
			progress.Visibility = ViewStates.Visible;
			progress.Indeterminate = true;
			List<DiemMon> list = new List<DiemMon>();
			if (Common.checkNWConnection (Activity) == true && autoupdate == true) {
				var rs=await BDiemThi.MakeDataFromXml (SQLite_Android.GetConnection ());
				if (rs==null)
				{
					Toast.MakeText (Activity, "Xảy ra lỗi trong quá trình cập nhật dữ liệu từ server", ToastLength.Long).Show();
				}
			}
			List<DiemThi> listDT = new List<DiemThi>();

			listDT = BDiemThi.getAll(SQLite_Android.GetConnection ());
			if (listDT.Count > 0) {
				linear.Visibility = ViewStates.Visible;
				txtNotify.Visibility = ViewStates.Gone;
				foreach (var item in listDT) {
				
					DiemMon Header = new DiemMon ();
					Header.Hocky = item.Hocky;
					Header.NamHoc = item.NamHoc;
					Header.MaMH = "Header";
					list.Add (Header);
					list.AddRange (BDiemThi.GetDiemMons (SQLite_Android.GetConnection (), item.Hocky, item.NamHoc));
					DiemMon Footer = new DiemMon ();
					if (item.Hocky != "3") {
						Footer.Hocky = item.Hocky;
						Footer.NamHoc = item.NamHoc;
						Footer.MaMH = "Footer";
						list.Add (Footer);
					}

				}
				listView.Adapter = new DiemThiApdater (Activity, list); 
			} else {
				linear.Visibility = ViewStates.Gone;
				txtHocKyDT.Visibility=ViewStates.Gone;
				txtNotify.Visibility = ViewStates.Visible;
				txtNotify.Text = "Hiện tại điểm thi chưa có dữ liệu. Xin vui lòng thử lại sau!!!";
			}
			progress.Indeterminate = false;
			progress.Visibility = ViewStates.Gone;
		}

		public static DiemThiFragment Instance
		{
			get
			{
				if (instance == null)
					instance = new DiemThiFragment ();
				return instance;
			}
		}

	}
}


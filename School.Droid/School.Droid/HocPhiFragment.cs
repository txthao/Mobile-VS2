
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
	public class HocPhiFragment : Fragment
	{
		ListView listView;
		ProgressBar progress;
		View rootView;
		TextView txtHocKyHP;
		bool check,autoupdate;
		public override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			// Create your fragment here
		}

		public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			// Use this to return your custom view for this Fragment
			// return inflater.Inflate(Resource.Layout.YourFragment, container, false);

			 rootView = inflater.Inflate(Resource.Layout.HocPhi, container, false);

			Bundle bundle=this.Arguments;
			check = bundle.GetBoolean ("Remind");
			autoupdate = bundle.GetBoolean ("AutoUpdateData");

			progress=rootView.FindViewById<ProgressBar>(Resource.Id.progressHP);
			listView = rootView.FindViewById<ListView>(Resource.Id.listHP);
			txtHocKyHP = rootView.FindViewById<TextView>(Resource.Id.txtHocKyHP);
			TextView txtNotify = rootView.FindViewById<TextView> (Resource.Id.txtNotify_HP);
			LinearLayout linear = rootView.FindViewById<LinearLayout> (Resource.Id.linear10);
			//load data
			HocPhi hp = BHocPhi.GetHP(SQLite_Android.GetConnection ());
			if (hp != null) {
				txtNotify.Visibility = ViewStates.Gone;
				linear.Visibility = ViewStates.Visible;
				LoadData ();
			} else {
				linear.Visibility = ViewStates.Gone;
				progress.Visibility = ViewStates.Gone;
				txtNotify.Visibility = ViewStates.Visible;
				txtNotify.Text = "Hiện tại học phí chưa có dữ liệu. Xin vui lòng thử lại sau!!!";
			}
			return rootView;
		}

		async void LoadData()
		{
			progress.Visibility = ViewStates.Visible;
			progress.Indeterminate = true;
			if (Common.checkNWConnection (Activity) == true && autoupdate == true) {
				
				 await BHocPhi.MakeDataFromXml (SQLite_Android.GetConnection ());
			}
			HocPhi hp = BHocPhi.GetHP(SQLite_Android.GetConnection ());
			List<CTHocPhi> listCT = BHocPhi.GetCTHP (SQLite_Android.GetConnection (), hp.NamHoc, hp.HocKy);
			HocPhiAdapter adapter = new HocPhiAdapter(Activity, listCT);

			listView.Adapter = adapter;
			rootView.FindViewById<TextView> (Resource.Id.txtHocKyHP).Text = "Học Kỳ " + hp.HocKy +" Năm Học "+ hp.NamHoc;
			progress.Visibility = ViewStates.Gone;
		}
	}
}


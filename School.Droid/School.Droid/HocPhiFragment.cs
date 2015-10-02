
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

		

			progress=rootView.FindViewById<ProgressBar>(Resource.Id.progressHP);
			listView = rootView.FindViewById<ListView>(Resource.Id.listHP);
			txtHocKyHP = rootView.FindViewById<TextView>(Resource.Id.txtHocKyHP);

			LoadData ();
			return rootView;
		}

		async void LoadData()
		{
			progress.Visibility = ViewStates.Visible;
			progress.Indeterminate = true;

			var t=await BHocPhi.MakeDataFromXml(SQLite_Android.GetConnection ());

			HocPhi hp = BHocPhi.GetHP(SQLite_Android.GetConnection ());
			List<CTHocPhi> listCT = BHocPhi.GetCTHP (SQLite_Android.GetConnection (), hp.NamHoc, hp.HocKy);
			HocPhiAdapter adapter = new HocPhiAdapter(Activity, listCT);

			listView.Adapter = adapter;
			rootView.FindViewById<TextView> (Resource.Id.txtHocKyHP).Text = "Học Kỳ " + hp.HocKy +" Năm Học "+ hp.NamHoc;
			rootView.FindViewById<TextView> (Resource.Id.txtTSTC).Text += hp.TongSoTC;
			rootView.FindViewById<TextView> (Resource.Id.txtTSTHP).Text += hp.TongSoTien;
			rootView.FindViewById<TextView> (Resource.Id.txtTTLD).Text += hp.TienDongTTLD;
			rootView.FindViewById<TextView> (Resource.Id.txtTDD).Text += hp.TienDaDong;
			rootView.FindViewById<TextView> (Resource.Id.txtTCN).Text += hp.TienConNo;
			progress.Indeterminate = false;
			progress.Visibility = ViewStates.Gone;
		}
	}
}


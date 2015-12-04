
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
using System.Threading.Tasks;

namespace School.Droid
{
	public class HocPhiFragment : Fragment
	{
		ListView listView;
		ProgressBar progress;
		View rootView;
		TextView txtHocKyHP;
		bool check,autoupdate;
		HocPhi hp;
		List<CTHocPhi> listCT;
		TextView txtNotify;
		LinearLayout linear;
		public static HocPhiFragment instance;

		public HocPhiFragment () 
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

			 rootView = inflater.Inflate(Resource.Layout.HocPhi, container, false);

			Bundle bundle=this.Arguments;
			check = bundle.GetBoolean ("Remind");
			autoupdate = bundle.GetBoolean ("AutoUpdateData");

			progress=rootView.FindViewById<ProgressBar>(Resource.Id.progressHP);
			listView = rootView.FindViewById<ListView>(Resource.Id.listHP);
			txtHocKyHP = rootView.FindViewById<TextView>(Resource.Id.txtHocKyHP);
			 txtNotify = rootView.FindViewById<TextView> (Resource.Id.txtNotify_HP);
			linear = rootView.FindViewById<LinearLayout> (Resource.Id.linear10);
			//load data
		
		
			progress.Visibility = ViewStates.Visible;
			//progress.Indeterminate = true;
			LoadData ();

			return rootView;
		}

		async void LoadData()
		{
			
			if (Common.checkNWConnection (Activity) == true && autoupdate == true) {
				
				var rs= await BHocPhi.MakeDataFromXml (SQLite_Android.GetConnection ());
				if (rs==null)
				{
					Toast.MakeText (Activity, "Xảy ra lỗi trong quá trình cập nhật dữ liệu từ server", ToastLength.Long).Show();
				}
			}

			hp = BHocPhi.GetHP(SQLite_Android.GetConnection ());
			progress.Visibility = ViewStates.Gone;
			if (hp != null) {
				listCT = BHocPhi.GetCTHP (SQLite_Android.GetConnection (), hp.NamHoc, hp.HocKy);
				txtNotify.Visibility = ViewStates.Gone;
				linear.Visibility = ViewStates.Visible;
				HocPhiAdapter adapter = new HocPhiAdapter (Activity, listCT);
				rootView.FindViewById<TextView> (Resource.Id.txtHocKyHP).Text = "Học Kỳ " + hp.HocKy + " Năm Học " + hp.NamHoc;
				listView.Adapter = adapter;
			} else {
			linear.Visibility = ViewStates.Gone;
			progress.Visibility = ViewStates.Gone;
			txtNotify.Visibility = ViewStates.Visible;
			txtNotify.Text = "Hiện tại học phí chưa có dữ liệu. Xin vui lòng thử lại sau!!!";
			}

		}

	
		public static HocPhiFragment Instance
		{
			get
			{
				if (instance == null)
					instance = new HocPhiFragment ();
				return instance;
			}
		}
	}
}


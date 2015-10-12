
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
		ListView listView;
		ProgressBar progress;
		TextView txtHocKy;
		bool check,autoupdate;
		public override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

		

		}

		public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			// Use this to return your custom view for this Fragment
			// return inflater.Inflate(Resource.Layout.YourFragment, container, false);
		
  
			var rootView = inflater.Inflate(Resource.Layout.LichThi, container, false);
		
			txtHocKy = rootView.FindViewById<TextView> (Resource.Id.txtHocKy);
            listView = rootView.FindViewById<ListView>(Resource.Id.listLT);
			progress= rootView.FindViewById<ProgressBar>(Resource.Id.progressLT);

			Bundle bundle=this.Arguments;
			check = bundle.GetBoolean ("Remind");
			autoupdate = bundle.GetBoolean ("AutoUpdateData");

			LoadData ();
			// row click
			listView.ItemClick += listView_ItemClick;
			return rootView;
		}
		void listView_ItemClick(object sender, AdapterView.ItemClickEventArgs e){

		}


		async void LoadData()
		{
			progress.Visibility = ViewStates.Visible;
			progress.Indeterminate = true;
			List<LichThi> list = new List<LichThi>();
			if (Common.checkNWConnection (Activity) == true && autoupdate == true) {
				await BLichThi.MakeDataFromXml (SQLite_Android.GetConnection ());
			}
			//	LichThi lt = BLichThi.GetNewestLT (SQLite_Android.GetConnection ()); chi hien thi lich thi moi nhat 
			list = BLichThi.getAll (SQLite_Android.GetConnection ());

		
			LichThiAdapter adapter = new LichThiAdapter(Activity, list);
			listView.Adapter = adapter;
			progress.Indeterminate = false;
			progress.Visibility = ViewStates.Gone;
		}
	}
}


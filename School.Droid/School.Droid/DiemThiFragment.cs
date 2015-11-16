
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
		bool check,autoupdate;
		Bundle bundle;
		public override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			// Create your fragment here
		}

		public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			// Use this to return your custom view for this Fragment
			// return inflater.Inflate(Resource.Layout.YourFragment, container, false);



			var rootView = inflater.Inflate(Resource.Layout.DiemThi, container, false);
			bundle = this.Arguments;
			check = bundle.GetBoolean ("Remind");
			autoupdate = bundle.GetBoolean ("AutoUpdateData");

			listView = rootView.FindViewById<ListView>(Resource.Id.listDT);
			progress=rootView.FindViewById<ProgressBar>(Resource.Id.progressDT);
			linear = rootView.FindViewById<LinearLayout>(Resource.Id.linear11);
			txtNotify = rootView.FindViewById<TextView>(Resource.Id.txtNotify_DT);
			LoadData ();

			//radio button
			RadioButton rb_All = rootView.FindViewById<RadioButton> (Resource.Id.rb_dangAll_All_DT);
			RadioButton rb_HocKy = rootView.FindViewById<RadioButton> (Resource.Id.rb_dangHK_All_DT);
			rb_All.Checked = true;
			rb_HocKy.Click += new EventHandler (rd_OnCheckedChangeListener);


			return rootView;
		}


		void rd_OnCheckedChangeListener (object sender, EventArgs e)
		{
			DiemThiHKFragment fragment = new DiemThiHKFragment();
			fragment.Arguments = bundle;
			FragmentManager.BeginTransaction()
				.Replace(Resource.Id.content_frame, fragment).AddToBackStack("31")
				.Commit();
		}
			
		async void LoadData()
		{
			progress.Visibility = ViewStates.Visible;
			progress.Indeterminate = true;
			List<DiemMon> list = new List<DiemMon>();
			if (Common.checkNWConnection (Activity) == true && autoupdate == true) {
				await BDiemThi.MakeDataFromXml (SQLite_Android.GetConnection ());
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
				//listView.SetAdapter (new DiemThiApdater (Activity, list)); 
			} else {
				linear.Visibility = ViewStates.Gone;
				txtNotify.Visibility = ViewStates.Visible;
				txtNotify.Text = "Hiện tại điểm thi chưa có dữ liệu. Xin vui lòng thử lại sau!!!";
			}
			progress.Indeterminate = false;
			progress.Visibility = ViewStates.Gone;
		}

	}
}


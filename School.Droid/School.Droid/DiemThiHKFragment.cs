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
	public class DiemThiHKFragment : Fragment
	{
		ListView listViewDT;
		TextView lbl_HK;
		TextView lbl_NH;
		bool check,autoupdate;
		Button btnHKKe;
		int btn_value ;
		ProgressBar progress;
		Bundle bundle;
		LinearLayout linear;
		TextView txtNotify;
		LinearLayout linearDT;
		Button btnHKTruoc;
		string lasthk="0",lastnh="0";
		bool flag = true;//check the first time and have data
		public static DiemThiHKFragment instance;

		public DiemThiHKFragment () 
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

			//	  set View
			var rootView = inflater.Inflate (Resource.Layout.DiemThi_HK, container, false);
			listViewDT = rootView.FindViewById<ListView> (Resource.Id.listDT_HK);
			lbl_HK = rootView.FindViewById<TextView> (Resource.Id.lbl_HK_DT);
			lbl_NH = rootView.FindViewById<TextView> (Resource.Id.lbl_NH_DT);
			progress=rootView.FindViewById<ProgressBar>(Resource.Id.progressDTHK);
			 txtNotify = rootView.FindViewById<TextView> (Resource.Id.txtNotify_DT_HK);
			linear = rootView.FindViewById<LinearLayout> (Resource.Id.linear_HK_DT);
			 linearDT = rootView.FindViewById<LinearLayout> (Resource.Id.linearDT_HK);
		//	RadioGroup radioGroup = rootView.FindViewById<RadioGroup> (Resource.Id.radioGroup3);
			//button 
			 btnHKTruoc = rootView.FindViewById<Button> (Resource.Id.btnHK_Truoc_DT);
			btnHKKe = rootView.FindViewById<Button> (Resource.Id.btnHK_Ke_DT);
			//bundle
			bundle=this.Arguments;
			check = bundle.GetBoolean ("Remind");
			autoupdate = bundle.GetBoolean ("AutoUpdateData");
			//load data
			LoadData ("0", "0");



			//radio button
//			RadioButton rb_tuan = rootView.FindViewById<RadioButton> (Resource.Id.rb_dangAll_HK_DT);
//			RadioButton rb_hocKy = rootView.FindViewById<RadioButton> (Resource.Id.rb_dangHK_HK_DT);
//			rb_hocKy.Checked = true;
//			rb_tuan.Click += new EventHandler (rd_OnCheckedChangeListener);

			//button event
			btnHKTruoc.Click += new EventHandler (btnHK_Truoc_Click);
			btnHKKe.Click += new EventHandler (btnHK_Ke_Click);	


			return rootView;
		}

//		void rd_OnCheckedChangeListener (object sender, EventArgs e)
//		{
//			DiemThiFragment fragment = new DiemThiFragment ();
//			fragment.Arguments = bundle;
//			FragmentManager.BeginTransaction ()
//				.Replace (Resource.Id.content_frame, fragment).AddToBackStack("32")
//				.Commit ();
//		}

		void btnHK_Truoc_Click (object sender, EventArgs e)
		{
			string hK;
			string nH;
			Common.calSemester (lbl_HK.Text, lbl_NH.Text, -1, out hK, out nH);
			btn_value = -1;
			LoadData (hK, nH);
		}

		void btnHK_Ke_Click (object sender, EventArgs e)
		{

			string hK;
			string nH;
			Common.calSemester (lbl_HK.Text, lbl_NH.Text, 1, out hK, out nH);
			btn_value = 1;
			LoadData (hK, nH);

		}


		async void LoadData (string hocKy, string namHoc)
		{
			listViewDT.Visibility = ViewStates.Invisible;
			progress.Visibility = ViewStates.Visible;
			progress.Indeterminate = true;
			DiemThi diemThi = new DiemThi ();
			if (Common.checkNWConnection (Activity) == true && autoupdate == true) {
				var rs= await BDiemThi.MakeDataFromXml (SQLite_Android.GetConnection ());
				if (rs==null)
				{
					Toast.MakeText (Activity, "Xảy ra lỗi trong quá trình cập nhật dữ liệu từ server", ToastLength.Long).Show();
				}
			}
			if (hocKy == "0") {
				diemThi = BDiemThi.GetNewestDT (SQLite_Android.GetConnection ());
				btnHKKe.Enabled = false;
				lastnh=diemThi.NamHoc;
				lasthk=diemThi.Hocky;
			} else {
				diemThi = BDiemThi.GetDT (SQLite_Android.GetConnection (), hocKy, namHoc);
				btnHKKe.Enabled = true;
				btnHKTruoc.Enabled = true;
				btnHKTruoc.SetBackgroundResource(Android.Resource.Color.HoloBlueDark);
				btnHKKe.SetBackgroundResource (Android.Resource.Color.HoloBlueDark);
				int t=0;
				while (diemThi == null&&t<3) {
					string hK;
					string nH;
					Common.calSemester (hocKy, namHoc, btn_value, out hK, out nH);
					diemThi = BDiemThi.GetDT (SQLite_Android.GetConnection (), hK, nH);
					t++;
				}
				if (t == 3 && diemThi == null) {
					btnHKTruoc.Enabled = false;
					btnHKTruoc.SetBackgroundResource(Android.Resource.Color.DarkerGray);
				}
			}

			lbl_HK.Text = hocKy;
			lbl_NH.Text = namHoc;
			if (diemThi != null) {
			if (lastnh.Equals (diemThi.NamHoc) && lasthk.Equals (diemThi.Hocky)) {
					btnHKKe.Enabled = false;
					btnHKKe.SetBackgroundResource(Android.Resource.Color.DarkerGray);
				}
			listViewDT.Visibility=ViewStates.Visible;
			List<DiemMon> listDM = BDiemThi.GetDiemMons (SQLite_Android.GetConnection (), diemThi.Hocky, diemThi.NamHoc);
			lbl_HK.Text = diemThi.Hocky;
			lbl_NH.Text = diemThi.NamHoc;
			DiemThiHKAdapter adapter = new DiemThiHKAdapter (Activity, listDM);
			listViewDT.Adapter = adapter;
			txtNotify.Visibility = ViewStates.Gone;
			//	radioGroup.Visibility = ViewStates.Visible;
			linear.Visibility = ViewStates.Visible;
			linearDT.Visibility = ViewStates.Visible;
			
		} else {
				
			linear.Visibility = ViewStates.Gone;
			linearDT.Visibility = ViewStates.Gone;
				listViewDT.Visibility=ViewStates.Gone;
			//	radioGroup.Visibility = ViewStates.Gone;
			
			txtNotify.Visibility = ViewStates.Visible;
			txtNotify.Text = "Hiện tại điểm thi chưa có dữ liệu. Xin vui lòng thử lại sau!!!";
			
		}
			progress.Indeterminate = false;
			progress.Visibility = ViewStates.Gone;



		}
		public static DiemThiHKFragment Instance
		{
			get
			{
				if (instance == null)
					instance = new DiemThiHKFragment ();
				return instance;
			}
		}
	}
}
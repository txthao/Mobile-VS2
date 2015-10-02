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
		TextView txt_TB10;
		TextView txt_TB4;
		TextView txt_TBTL10;
		TextView txt_TBTL4;
		TextView txt_DRL;
		int btn_value ;
			ProgressBar progress;

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
			txt_TB10 = rootView.FindViewById<TextView> (Resource.Id.txtTB10_HK);
			txt_TB4 = rootView.FindViewById<TextView> (Resource.Id.txtTB4_HK);
			txt_TBTL10 = rootView.FindViewById<TextView> (Resource.Id.txtTBTL10_HK);
			txt_TBTL4 = rootView.FindViewById<TextView> (Resource.Id.txtTBTL4_HK);
			txt_DRL = rootView.FindViewById<TextView> (Resource.Id.txtDRL_HK);
			progress=rootView.FindViewById<ProgressBar>(Resource.Id.progressDTHK);

			//load data
			DiemThi dt = BDiemThi.GetNewestDT (SQLite_Android.GetConnection ());
			if (dt != null) {
				LoadData (dt.Hocky, dt.NamHoc);
			} else {
				LoadData ("0", "0");
			}

			//radio button
			RadioButton rb_tuan = rootView.FindViewById<RadioButton> (Resource.Id.rb_dangAll_HK_DT);
			RadioButton rb_hocKy = rootView.FindViewById<RadioButton> (Resource.Id.rb_dangHK_HK_DT);
			rb_hocKy.Checked = true;
			rb_tuan.Click += new EventHandler (rd_OnCheckedChangeListener);

			//button 
			Button btnHKTruoc = rootView.FindViewById<Button> (Resource.Id.btnHK_Truoc_DT);
			btnHKTruoc.Click += new EventHandler (btnHK_Truoc_Click);
			Button btnHKKe = rootView.FindViewById<Button> (Resource.Id.btnHK_Ke_DT);
			btnHKKe.Click += new EventHandler (btnHK_Ke_Click);	
		

			return rootView;
		}

		void rd_OnCheckedChangeListener (object sender, EventArgs e)
		{
			DiemThiFragment fragment = new DiemThiFragment ();
			FragmentManager.BeginTransaction ()
				.Replace (Resource.Id.content_frame, fragment)
				.Commit ();
		}

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

			DiemThi diemThi = new DiemThi ();
			var t = await BDiemThi.MakeDataFromXml(SQLite_Android.GetConnection ());
			if (hocKy == "0") {
				diemThi = BDiemThi.GetNewestDT (SQLite_Android.GetConnection ());
			} else {
				diemThi = BDiemThi.GetDT (SQLite_Android.GetConnection (), hocKy, namHoc);
				while (diemThi == null) {
					string hK;
					string nH;
					Common.calSemester (hocKy, namHoc, btn_value, out hK, out nH);
					diemThi = BDiemThi.GetDT (SQLite_Android.GetConnection (), hK, nH);
				}
			}
			List<DiemMon> listDM = BDiemThi.GetDiemMons (SQLite_Android.GetConnection (), diemThi.Hocky, diemThi.NamHoc);
			lbl_HK.Text = diemThi.Hocky;
			lbl_NH.Text = diemThi.NamHoc;
			txt_TB10.Text = diemThi.DiemTB10;
			txt_TB4.Text = diemThi.DiemTB4;
			txt_TBTL10.Text = diemThi.DiemTBTL10;
			txt_TBTL4.Text = diemThi.DiemTBTL4;
			txt_DRL.Text = diemThi.DiemRL;
			DiemThiHKAdapter adapter = new DiemThiHKAdapter (Activity, listDM);
			listViewDT.Adapter = adapter;  
				progress.Visibility = ViewStates.Gone;


		}

//		async void LoadData (string hocKy, string namHoc)
//		{
//			System.Diagnostics.Debug.WriteLine ("test: " + hocKy + " " + namHoc);
//			DiemThi diemThi = new DiemThi ();
//			var t = await BDiemThi.MakeDataFromXml(SQLite_Android.GetConnection ());
//			if (hocKy == "0") {
//				diemThi = BDiemThi.GetNewestDT (SQLite_Android.GetConnection ());
//			} else {
//				diemThi = BDiemThi.GetDT (SQLite_Android.GetConnection (), hocKy, namHoc);
//			}
//			List<DiemMon> listDM = BDiemThi.GetDiemMons (SQLite_Android.GetConnection (), diemThi.Hocky, diemThi.NamHoc);
//			lbl_HK.Text = hocKy;
//			lbl_NH.Text = namHoc;
//			txt_TB10.Text = diemThi.DiemTB10;
//			txt_TB4.Text = diemThi.DiemTB4;
//			txt_TBTL10.Text = diemThi.DiemTBTL10;
//			txt_TBTL4.Text = diemThi.DiemTBTL4;
//			txt_DRL.Text = diemThi.DiemRL;
//			DiemThiHKAdapter adapter = new DiemThiHKAdapter (Activity, listDM);
//			listView.Adapter = adapter;  
//			//progress.Visibility = ViewStates.Gone;
//
//
//		}
	}
}

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

namespace School.Droid
{
	public class SettingsFragment : Fragment
	{
		bool autoupdate;
		CheckBox cbNLT,cbUpdate;
		Button btupdateData;
		ProgressBar progressup;
		TextView txtResult;
		public override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			// Create your fragment here
		}

		public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			// Use this to return your custom view for this Fragment
			// return inflater.Inflate(Resource.Layout.YourFragment, container, false);
			var rootView = inflater.Inflate(Resource.Layout.Settings, container, false);
			cbNLT = rootView.FindViewById<CheckBox> (Resource.Id.ckboxRemindLT);
			cbUpdate=rootView.FindViewById<CheckBox> (Resource.Id.ckboxAutoUpdateData);
			btupdateData=rootView.FindViewById<Button> (Resource.Id.btUpdateData);
			progressup=rootView.FindViewById<ProgressBar> (Resource.Id.proUpdateData);
			txtResult = rootView.FindViewById<TextView> (Resource.Id.txtresult);
			cbUpdate.CheckedChange+= CbUpdate_CheckedChange;
			cbNLT.CheckedChange += CbNLT_CheckedChange;
			Bundle bundle=this.Arguments;
			bool check = bundle.GetBoolean ("Remind");
			 autoupdate = bundle.GetBoolean ("AutoUpdateData");
			cbUpdate.Checked = autoupdate;
			cbNLT.Checked = check;
			btupdateData.Click+= BtupdateData_Click;

			return rootView;
		}

		async void BtupdateData_Click (object sender, EventArgs e)
		{
			progressup.Visibility= ViewStates.Visible;
			progressup.Indeterminate = true;
			if (Common.checkNWConnection (Activity) == true) {
				await Common.LoadDataFromSV ();
				txtResult.Text = "Cập Nhật Dữ Liệu Thành Công";
			} else {
				txtResult.Text = "Không Có Kết Nối Mạng, Vui Lòng Thử Lại Sau";

			}
			progressup.Indeterminate = false;
			progressup.Visibility = ViewStates.Gone;
		}

		void CbUpdate_CheckedChange (object sender, CompoundButton.CheckedChangeEventArgs e)
		{
			if (cbUpdate.Checked == true) {
				btupdateData.Visibility = ViewStates.Invisible;
			} else {
				btupdateData.Visibility = ViewStates.Visible;

			}
			var prefs = Application.Context.GetSharedPreferences("SGU APP", FileCreationMode.Private);
			var prefEditor = prefs.Edit();
			prefEditor.PutBoolean("AutoUpdateData",cbUpdate.Checked);
			prefEditor.Commit();
		}


		void CbNLT_CheckedChange (object sender, CompoundButton.CheckedChangeEventArgs e)
		{
			if (cbNLT.Checked == true) {
				
				try{
					List<LichThi> listlt= BLichThi.getAll(SQLite_Android.GetConnection());
					Log.Debug("logsettings","Load LT Success");
					List<LichHoc> listlh= BLichHoc.GetNewestLH(SQLite_Android.GetConnection());
					Log.Debug("logsettings","Load LH Success");
					ScheduleReminder.RemindAllLT(Activity,listlt);
					ScheduleReminder.RemindAllLH(Activity,listlh);
					txtResult.Text="Cài Đặt Nhắc Lịch Hoàn Tất";

				}
				catch {
					
				}

			} else {
				
				ScheduleReminder.DeleteAlLRemind (Activity);
				txtResult.Text="Xoá Nhắc Lịch Hoàn Tất";
			}
			var prefs = Application.Context.GetSharedPreferences("SGU APP", FileCreationMode.Private);
			var prefEditor = prefs.Edit();
			prefEditor.PutBoolean("Remind",cbNLT.Checked);
			prefEditor.Commit();
		}

	
	}
}


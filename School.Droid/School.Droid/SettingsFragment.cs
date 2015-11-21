
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

using Android.Content.PM;

namespace School.Droid
{
	public class SettingsFragment : Fragment
	{
		bool autoupdate;
		CheckBox cbNLT,cbUpdate;
		Button btupdateData;
		ProgressBar progressup,progressNL;
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
			progressNL=rootView.FindViewById<ProgressBar> (Resource.Id.proNL);
			//About
			TextView txtVersion = rootView.FindViewById<TextView> (Resource.Id.txtVersion_Set);
			txtVersion.Click += TxtVersion_Click;
			Bundle bundle=this.Arguments;
			bool check = bundle.GetBoolean ("Remind");
			 autoupdate = bundle.GetBoolean ("AutoUpdateData");
			cbUpdate.Checked = autoupdate;
			cbNLT.Checked = check;
			btupdateData.Click+= BtupdateData_Click;
			cbNLT.CheckedChange += CbNLT_CheckedChange;
			cbUpdate.CheckedChange+= CbUpdate_CheckedChange;
			btupdateData.Enabled = !cbUpdate.Checked;
			return rootView;
		}

		void TxtVersion_Click (object sender, EventArgs e)
		{
			Dialog dialog = new Dialog(this.Activity);
			dialog.SetContentView(Resource.Layout.CustomDialog);
			dialog.SetTitle("Giới thiệu");




			dialog.FindViewById<TextView> (Resource.Id.txt_d1).Text = UrlHelper.About();

			dialog.Show();
		}

		async void BtupdateData_Click (object sender, EventArgs e)
		{
			progressup.Visibility= ViewStates.Visible;
			progressup.Indeterminate = true;
			if (Common.checkNWConnection (Activity) == true) {
				await Common.LoadDataFromSV (Activity);


			} else {
				txtResult.Text = "Không Có Kết Nối Mạng, Vui Lòng Thử Lại Sau";

			}
			progressup.Indeterminate = false;
			progressup.Visibility = ViewStates.Gone;
		}

		void CbUpdate_CheckedChange (object sender, CompoundButton.CheckedChangeEventArgs e)
		{
			btupdateData.Enabled = !cbUpdate.Checked;
			var prefs = Application.Context.GetSharedPreferences("SGU APP", FileCreationMode.Private);
			var prefEditor = prefs.Edit();
			prefEditor.PutBoolean("AutoUpdateData",cbUpdate.Checked);
			prefEditor.Commit();
		}


		async void CbNLT_CheckedChange (object sender, CompoundButton.CheckedChangeEventArgs e)
		{
			progressNL.Visibility= ViewStates.Visible;
			progressNL.Indeterminate = true;
			if (cbNLT.Checked == true) {

				try{
					List<LichThi> listlt= BLichThi.GetNewestLT(SQLite_Android.GetConnection());
					Log.Debug("logsettings","Load LT Success");
					List<LichHoc> listlh= BLichHoc.GetNewestLH(SQLite_Android.GetConnection());
					Log.Debug("logsettings","Load LH Success");
					ScheduleReminder reminder = new ScheduleReminder(Activity);
					await reminder.RemindAllLH(listlh);
					await reminder.RemindAllLT(listlt);
					Toast.MakeText (Activity, "Cài đặt nhắc lịch hoàn tất", ToastLength.Long).Show();
				}
				catch {

				}

			} else {
				
				ScheduleReminder reminder = new ScheduleReminder(Activity);
				await reminder.DeleteAlLRemind ();
				Toast.MakeText (Activity, "Xoá nhắc lịch hoàn tất", ToastLength.Long).Show();
			}
			progressNL.Indeterminate = false;
			progressNL.Visibility = ViewStates.Gone;
			var prefs = Application.Context.GetSharedPreferences("SGU APP", FileCreationMode.Private);
			var prefEditor = prefs.Edit();
			prefEditor.PutBoolean("Remind",cbNLT.Checked);
			prefEditor.Commit();
		}

	
	}
}



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

namespace School.Droid
{
	public class SettingsFragment : Fragment
	{
		CheckBox cbNLT,cbNLH;
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
	//		cbNLH = rootView.FindViewById<CheckBox> (Resource.Id.ckboxRemindLH);
			cbNLT.CheckedChange += CbNLT_CheckedChange;
			Bundle bundle=this.Arguments;
			bool check = bundle.GetBoolean ("Remind");
			cbNLT.Checked = check;

	//		cbNLH.CheckedChange += new EventHandler (ChangeCBNLH);
			return rootView;
		}

		void CbNLT_CheckedChange (object sender, CompoundButton.CheckedChangeEventArgs e)
		{
			if (cbNLT.Checked == true) {
				ScheduleReminder.setupAlarm (Activity);


			} else {
				ScheduleReminder.stopAlarm (Activity);
			}
			var prefs = Application.Context.GetSharedPreferences("SGU APP", FileCreationMode.Private);
			var prefEditor = prefs.Edit();
			prefEditor.PutBoolean("Remind",cbNLT.Checked);
			prefEditor.Commit();
		}

	
	}
}


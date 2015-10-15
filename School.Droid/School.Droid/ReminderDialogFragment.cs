using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Graphics.Drawables;
using Android.Graphics;
using School.Core;


namespace School.Droid
{
		
	public class ReminderDialogFragment : DialogFragment
	{
		String maMH;
		String namHoc;
		String hocKy;
		Bundle bundle;
		Boolean check;
		MonHoc mh;
		LichHoc lh;
		LichThi lt;
		EditText minutes;
		EditText content;
		Button Cancel;
		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			var view = inflater.Inflate(Resource.Layout.Reminder_Dialog, container, false);

			Button Save = view.FindViewById<Button> (Resource.Id.btnSave);
			 Cancel = view.FindViewById<Button> (Resource.Id.btnCancel);
			 content = view.FindViewById<EditText> (Resource.Id.edtxt_Content);
			TextView title = view.FindViewById<TextView> (Resource.Id.txtTitleRM);
			TextView subject = view.FindViewById<TextView> (Resource.Id.txtSubjectRM);
			TextView time = view.FindViewById<TextView> (Resource.Id.txtTimeRM);
			minutes = view.FindViewById<EditText> (Resource.Id.txt_minutes);
			bundle = this.Arguments;
			maMH = bundle.GetString ("MH");
			namHoc = bundle.GetString ("NH");
			hocKy = bundle.GetString ("HK");
			check = bundle.GetBoolean ("check");

			if (check) {
				mh = BMonHoc.GetMH (SQLite_Android.GetConnection (), maMH);
				lt = BLichThi.GetLichThi (SQLite_Android.GetConnection (), maMH);
				title.Text = "NHẮC LỊCH THI";
				subject.Text = "Môn:" + mh.TenMH;

				time.Text = "Thời gian: " + lt.GioBD + " Ngày: " + lt.NgayThi;// TACH THANH 2 DONG  DE NGAY TRUOC


			} else {
				lh = BLichHoc.GetLH (SQLite_Android.GetConnection (), maMH, namHoc, hocKy);
				mh = BMonHoc.GetMH (SQLite_Android.GetConnection (), maMH);
				title.Text = "NHẮC LỊCH HOC";
				subject.Text = "Môn: " + mh.TenMH;

			}
			Save.Click+= Save_Click;
			Cancel.Click += new EventHandler (btnCancel_OnClickListener);
			return view;
		}

		void btnCancel_OnClickListener(object sender, EventArgs e){
			
			Activity.OnBackPressed ();
		}

		void Save_Click (object sender, EventArgs e)
		{
			ScheduleReminder reminder = new ScheduleReminder (Activity);
			reminder.content = content.Text;
			reminder.MinutesRemind = int.Parse(minutes.Text.Trim());
			if (check) {
				

				reminder.lt = lt;
				reminder.SetCalenDarLT ();
			} else {
				reminder.lh = lh;
				List<LichHoc> list = new List<LichHoc> ();
				list.Add (lh);
				reminder.RemindAllLH (list);

			}
			Toast.MakeText (Activity, "Cài dặt nhắc lịch thành công", ToastLength.Long).Show();
			Cancel.CallOnClick ();
		}




	}
}


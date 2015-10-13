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


namespace School.Droid
{
		
	public class ReminderDialogFragment : DialogFragment
	{
		String tenMH;
		Bundle bundle;
		Boolean check;
		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			var view = inflater.Inflate(Resource.Layout.Reminder_Dialog, container, false);

			Button Save = view.FindViewById<Button> (Resource.Id.btnSave);
			Button Cancel = view.FindViewById<Button> (Resource.Id.btnCancel);
			EditText Content = view.FindViewById<EditText> (Resource.Id.edtxt_Content);

			bundle = this.Arguments;
			tenMH = bundle.GetString ("MH");
			check = bundle.GetBoolean ("check");
			Content.Text = tenMH;
			Cancel.Click += new EventHandler (btnCancel_OnClickListener);
			return view;
		}

		void btnCancel_OnClickListener(object sender, EventArgs e){
			if (check) {
				LichThiFragment fragment = new LichThiFragment ();
				fragment.Arguments = bundle;
				FragmentManager.BeginTransaction ()
				.Replace (Resource.Id.content_frame, fragment)
				.Commit ();
			} else {
				LichHocHKFragment fragment = new LichHocHKFragment ();
				fragment.Arguments = bundle;
				FragmentManager.BeginTransaction ()
					.Replace (Resource.Id.content_frame, fragment)
					.Commit ();
			}

		}


	}
}



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
using System.Threading;

namespace School.Droid
{
	[Activity (Label = "SGU Mobile",MainLauncher = true, 
		Theme = "@style/Theme.Splash",NoHistory = true)]			
	public class Main : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
//			Intent myintent = new Intent (this, typeof(DrawerActivity));
//			StartActivity (myintent);

//			this.Finish ();
			SetContentView (Resource.Layout.Menu);
			if (School.Core.BUser.IsLogined (SQLite_Android.GetConnection ()) == false) {

				Intent myintent = new Intent (this, typeof(LoginActivity));
				StartActivity (myintent);

				this.Finish ();

			} else {

				Intent myintent = new Intent (this, typeof(DrawerActivity));
				StartActivity (myintent);

				this.Finish ();
			}

		}
	}
}


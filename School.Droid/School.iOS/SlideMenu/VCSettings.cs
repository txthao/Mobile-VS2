
using System;

using Foundation;
using UIKit;
using System.Collections.Generic;
using School.Core;

namespace School.iOS
{
	public partial class VCSettings : UIViewController
	{
		
		public VCSettings () : base ("VCSettings", null)
		{
		}



		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			swtCNDL.On = SettingsHelper.LoadSetting ("AutoUpdate");
			swtNLich.On=SettingsHelper.LoadSetting ("Remind");
			btCNDL.Enabled = !swtCNDL.On;
			swtCNDL.ValueChanged+= SwtCNDL_ValueChanged;
			swtNLich.ValueChanged+= SwtNLich_ValueChanged;
			btCNDL.TouchUpInside+= BtCNDL_TouchUpInside;

			progress.Hidden = true;
			// Perform any additional setup after loading the view, typically from a nib.
		}

		async void SwtNLich_ValueChanged (object sender, EventArgs e)
		{
			SettingsHelper.SaveSetting ("Remind", swtNLich.On);
			progress.Hidden = false;
			progress.StartAnimating ();
			if (swtNLich.On) {
				try {
					List<LichThi> listlt = BLichThi.GetNewestLT (SQLite_iOS.GetConnection ());

					List<LichHoc> listlh = BLichHoc.GetNewestLH (SQLite_iOS.GetConnection ());
				
					VCHomeReminder reminder = new VCHomeReminder (this);
					await reminder.RemindALLLH (listlh, "");
					await reminder.RemindAllLT (listlt);
					progress.StopAnimating ();
				} catch {

				}
			} else {
				VCHomeReminder reminder = new VCHomeReminder (this);
				await reminder.RemoveAllEvent ();
				BRemind.RemoveAllRM (SQLite_iOS.GetConnection ());
				progress.StopAnimating ();
			}
		}

		void SwtCNDL_ValueChanged (object sender, EventArgs e)
		{
			btCNDL.Enabled = !swtCNDL.On;
			SettingsHelper.SaveSetting ("AutoUpdate", swtCNDL.On);
		}

		async void BtCNDL_TouchUpInside (object sender, EventArgs e)
		{
			
			progress.Hidden = false;
			progress.StartAnimating ();
			try
			{
			UIApplication.SharedApplication.NetworkActivityIndicatorVisible = true;
			var result= ApiHelper.LoadDataFromSV(this);
			txtResult.Text=await result;
			progress.StopAnimating ();
			UIApplication.SharedApplication.NetworkActivityIndicatorVisible = false;
			}
			catch {
			txtResult.Text = "Xảy Ra Lỗi Trong Quá Trình Load Dữ Liệu";
			}

		}

	}
}

